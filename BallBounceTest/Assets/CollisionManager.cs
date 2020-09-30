using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    PlaneScript plane;
    public Vector3 velocity;
    Vector3 acceleration = new Vector3(0, -9.8f, 0);
    float forceApplied = 50f;
    public float Radius_Of_Sphere = 0.5f;
    public Rigidbody sphere;
    private Vector3 lastFrameVelocity;
    private float Coefficient_of_Restitution = 0.6f;
    private Rigidbody rb;
    private void Start()
    {
        plane = FindObjectOfType<PlaneScript>();
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Detected!");
        if (col.gameObject.CompareTag("Ball"))
        {//doesnt work in the current implementation
            Debug.Log("Sphere tag triggered");
            Vector3 dir = col.contacts[0].point - transform.position;
            dir = -dir.normalized;
            col.gameObject.GetComponent<Rigidbody>().AddForce(dir * forceApplied);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Plane"))
        {
            Debug.Log("Plane tag triggered");

            Vector3 parallel = parallel_component(velocity, plane.normal);
            Vector3 perp = perpendicular_component(velocity, plane.normal);

            transform.position -= velocity * (Time.deltaTime);

            velocity = perp - Coefficient_of_Restitution * parallel;

            transform.position += velocity * (Time.deltaTime);
        }

        if (other.gameObject.CompareTag("Ball"))
        {
            rb = this.GetComponent<Rigidbody>();
            var force = transform.position - other.transform.position;
            force.Normalize();
            ReflectObject(rb, force);
            Debug.Log("Ball tag triggered");
            //var magnitude = 5000;
            //Vector3 test = new Vector3(3, 3, 3);
            
            //other.GetComponent<Rigidbody>().AddForce(force * magnitude);
            //transform.position += test;

           
            //Vector3 dir = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position) - transform.position;
            //Debug.Log(dir);
            //dir = -dir.normalized;
            //other.gameObject.GetComponent<Rigidbody>().AddForce(dir * forceApplied);
        }
    }
    void Update()
    {
        lastFrameVelocity = sphere.velocity;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }


    private void FixedUpdate()
    {
        if (sphere.GetComponent<Rigidbody>() == null || sphere.GetComponent<Rigidbody>().isKinematic)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }
    private Vector3 perpendicular_component(Vector3 vec, Vector3 normal)
    {
        return vec - parallel_component(vec, normal);
    }

    private Vector3 parallel_component(Vector3 vec, Vector3 normal)
    {
        return Vector3.Dot(vec, normal) * normal;
    }
    private void ReflectObject(Rigidbody rb, Vector3 reflectVector)
    {
        velocity = Vector3.Reflect(velocity, reflectVector);
        rb.velocity = velocity;
    }
}