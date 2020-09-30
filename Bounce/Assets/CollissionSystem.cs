using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class CollissionSystem : MonoBehaviour
    {
        PlaneScript plane;
        public Vector3 velocity;
        Vector3 acceleration = new Vector3(0, -9.8f, 0);
        public float Radius_Of_Sphere = 0.5f;
        public Rigidbody sphere;
        private Vector3 lastFrameVelocity;
        private float Coefficient_of_Restitution = 0.6f;
        private void Start()
        {
            plane = FindObjectOfType<PlaneScript>();
        }
        void OnCollisionEnter(Collision col)
        {
            Debug.Log("Collision Detected!");
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Sphere"))
            {
                Debug.Log("Sphere tag triggered");
                
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.AddExplosionForce(10f, transform.position, Radius_Of_Sphere, 3.0f);
            }
            if(other.gameObject.CompareTag("Plane"))
            {
                Debug.Log("Plane tag triggered");

                    Vector3 parallel = parallel_component(velocity, plane.normal);
                    Vector3 perp = perpendicular_component(velocity, plane.normal);

                    transform.position -= velocity * (Time.deltaTime);

                    velocity = perp - Coefficient_of_Restitution * parallel;

                    transform.position += velocity * (Time.deltaTime);
                
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

    }
}
