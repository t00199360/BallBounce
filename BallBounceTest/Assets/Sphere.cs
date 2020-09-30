using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    PlaneScript plane;

    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -9.8f, 0);

    float d1 = 0;
    float d2 = 0;

    private float Radius_Of_Sphere = 0.5f;
    private float Coefficient_of_Restitution = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        plane = FindObjectOfType<PlaneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        d1 = plane.distance_to(transform.position) - Radius_Of_Sphere;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        float distance_from_center_to_plane = plane.distance_to(transform.position);
        d2 = distance_from_center_to_plane - Radius_Of_Sphere;
        if (d2<=0)
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        Vector3 parallel = parallel_component(velocity, plane.normal);
        Vector3 perp = perpendicular_component(velocity, plane.normal);

        float time_of_impact = Time.deltaTime * d1 / (d1 - d2);
        transform.position -= velocity * (Time.deltaTime - time_of_impact);

        velocity = perp - Coefficient_of_Restitution * parallel;

        transform.position += velocity * (Time.deltaTime - time_of_impact);
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
