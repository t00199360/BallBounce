using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public static Vector3 point_on_sphere, normal_to_sphere;
    GameObject[] ListOfSpheres;
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
    
    }

    // Update is called once per frame
    void Update()
    {
        ListOfSpheres = GameObject.FindGameObjectsWithTag("Sphere");
    }


    public float distance_to(Vector3 point)
    {
        Vector3 point_on_sphere_to_point = point - point_on_sphere;

        return Vector3.Dot(point_on_sphere_to_point, normal_to_sphere);
    }

    public GameObject FindClosestSphere()
    {
        
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;


        foreach (GameObject sphere in ListOfSpheres)
        {
            Debug.Log("In list foreach");
            Vector3 diff = sphere.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = sphere;
                distance = curDistance;
            }
        }
        return FindSecondClosest(closest);
    }

    public GameObject FindSecondClosest(GameObject nearest)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject sphere in ListOfSpheres)
        {
            Vector3 diff = sphere.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance && sphere != nearest)
            {
                closest = sphere;
                distance = curDistance;
            }
        }
        Debug.Log("second closest to " + nearest  + " is " + closest);
        return closest;
    }
}
