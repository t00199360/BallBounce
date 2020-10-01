﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsControl : MonoBehaviour
{
    Plane plane;
    Sphere sphere;
    Vector3 velocity  = new Vector3(0,0,0);
    Vector3 acceleration = new Vector3(0, -9.8f, 0);

    private float Radius_Of_Sphere = 0.5f;
    private float Coefficient_of_Restitution = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        plane = FindObjectOfType<Plane>();

        sphere = FindObjectOfType<Sphere>();

    }

    // Update is called once per frame
    void Update()
    {
        float d1 = plane.distance_to(transform.position) - Radius_Of_Sphere;
        //CHECKPOINT
        //wrong because sphere .distance to own position is always <= 0. need to find the closest sphere
        //distance between two points, foreaach loop a list of all spheres to find the closest(smallest distance)
        float s1 = sphere.distance_to(transform.position) - Radius_Of_Sphere;

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        float distance_from_center_to_plane = plane.distance_to(transform.position);

        //                                      sphere needs to be nearestSphere
        float distance_from_center_to_sphere = sphere.distance_to(transform.position);
        float s2 = distance_from_center_to_sphere - Radius_Of_Sphere;
        float d2 = distance_from_center_to_plane - Radius_Of_Sphere;

        if (d2 <= 0)
        {
            Debug.Log("d2");
            Vector3 parallel = parallel_component(velocity, plane.normal);
            Vector3 perp = perpendicular_component(velocity, plane.normal);

            float time_of_impact = Time.deltaTime * d1 / (d1 - d2);
            transform.position -= velocity * (Time.deltaTime - time_of_impact);

            velocity = perp - Coefficient_of_Restitution * parallel;

            transform.position += velocity * (Time.deltaTime - time_of_impact);
        }
        if(s2 <= 0)
        {
            Debug.Log("s2");
            Vector3 parallel = parallel_component(velocity, sphere.normal);
            Vector3 perpendicular = perpendicular_component(velocity, sphere.normal);

            float time_of_impact = Time.deltaTime * s1 / (s1 - s2);
            transform.position -= velocity * (Time.deltaTime - time_of_impact);

            velocity = perpendicular - Coefficient_of_Restitution * parallel;

            transform.position += velocity * (Time.deltaTime - time_of_impact);
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
