using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Plane : MonoBehaviour
{
    Vector3 point_on_plane, normal_to_plane;

    public Vector3 normal
    {
        get
        {
            return normal_to_plane;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public float distance_to(Vector3 point)
    {
        Vector3 point_on_plane_to_point = point - point_on_plane;

        return Vector3.Dot(point_on_plane_to_point, normal_to_plane);
    }
}
