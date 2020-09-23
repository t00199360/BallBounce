using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics_Script : MonoBehaviour
{
    PlaneScript plane;
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -9.8f, 0);

   

    // Start is called before the first frame update
    void Start()
    {
        plane = FindObjectOfType<PlaneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if(plane.distance_to(transform.position)<= 0.5f)
        {
            transform.position -= velocity * Time.deltaTime;
            Vector3 parallel = parallel_component(velocity, plane.normal);
            Vector3 perp = perpendicular_component(velocity, plane.normal);

            velocity = perp - 0.6f * parallel;
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
