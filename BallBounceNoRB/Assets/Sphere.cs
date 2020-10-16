using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public static Vector3 point_on_sphere, normal_to_sphere;
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -9.8f, 0);
    Sphere closestSphere;

    List<Sphere> ListOfSpheres;
    List<Plane> ListOfPlanes;
    Plane closestPlane;

    private float Coefficient_of_Restitution = 0.6f;

    private float Radius_Of_Sphere = 0.5f;

    public Vector3 normal
    {
        get
        {
            return normal_to_sphere;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ListOfSpheres = new List<Sphere>(FindObjectsOfType<Sphere>());
        ListOfPlanes = new List<Plane>(FindObjectsOfType<Plane>());
    }


    // Update is called once per frame
    void Update()
    {
        closestPlane = FindClosestPlane();
        closestSphere = FindClosestSphere();
        Vector3 difference = closestSphere.transform.position - transform.position;
        float convertedDifference = difference.sqrMagnitude;

        float s1 = Vector3.Distance(transform.position, closestSphere.transform.position) - (Radius_Of_Sphere * 2);
        float d1 = distance_to(closestPlane.transform.position) - Radius_Of_Sphere;

        Vector3 sphereNormalized = (transform.position - closestSphere.transform.position).normalized;

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        float distance_from_center_to_sphere = Vector3.Distance(transform.position,closestSphere.transform.position);
        float s2 = distance_from_center_to_sphere - (Radius_Of_Sphere * 2);


        float distance_from_center_to_plane = closestPlane.distance_to(transform.position);
        float d2 = distance_from_center_to_plane - Radius_Of_Sphere;

        if (d2 < 0)
        {
            Vector3 parallel = parallel_component(velocity, closestPlane.normal);
            Vector3 perp = perpendicular_component(velocity, closestPlane.normal);

            float time_of_impact = Time.deltaTime * d1 / (d1 - d2);
            transform.position -= velocity * (Time.deltaTime - time_of_impact);

            velocity = perp - Coefficient_of_Restitution * parallel;

            transform.position += velocity * (Time.deltaTime - time_of_impact);
        }


        if (convertedDifference <= (Radius_Of_Sphere * 2))      //If check definitely works DO NOT CHANGE
        {
            Vector3 parallel = parallel_component(velocity, sphereNormalized);
            Vector3 perpendicular = perpendicular_component(velocity, sphereNormalized);
            //Debug.Log("s1 is: " + s1);
            //Debug.Log("s2 is: " + s2);

            float time_of_impact = Time.deltaTime * (s1 / (s1 - s2));
            //Debug.Log(time_of_impact + " is the time of impact");
            transform.position -= velocity * (Time.deltaTime - time_of_impact);

            velocity = perpendicular - Coefficient_of_Restitution * parallel;

            transform.position += velocity * (Time.deltaTime - time_of_impact);
        }
    }


    public float distance_to(Vector3 point)
    {
        Vector3 point_on_plane_to_point = point - point_on_sphere;

        return Vector3.Dot(point_on_plane_to_point, normal_to_sphere);
    }

    public Plane FindClosestPlane()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Plane plane in ListOfPlanes)
        {
            Vector3 diff = plane.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closestPlane = plane;
                distance = curDistance;
            }
        }
        return closestPlane.GetComponent<Plane>();
    }

    public Sphere FindClosestSphere()
    {
        Sphere closest = null;      //the closest sphere points at itself, the second closest is technically the actual closest
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;


        foreach (Sphere sphere in ListOfSpheres)    //iterating through the list of sphere with type 'sphere'
        {
            Vector3 diff = sphere.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = sphere;
                distance = curDistance;
            }
        }
        return FindSecondClosest(closest);  //calling the method that actually returns the closest
    }

    public Sphere FindSecondClosest(Sphere nearest)
    {
        Sphere closest = null;      //see above methods comments for explanation of why im retrieving the second closest
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Sphere sphere in ListOfSpheres)
        {
            Vector3 diff = sphere.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance && sphere != nearest)     //this logic exludes the "closest" if it matches its own position and then takes the next one.
            {
                closest = sphere;
                distance = curDistance;
            }
        }
        return closest;
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
