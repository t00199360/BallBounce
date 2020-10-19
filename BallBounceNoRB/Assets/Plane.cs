﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Plane : MonoBehaviour
{

    Vector3 point_on_plane, normal_to_plane;
    List<Plane> ListOfPlanes;

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
        ListOfPlanes = new List<Plane>(FindObjectsOfType<Plane>());     //repositions and rotates the planes appropriately
        foreach (Plane plane in ListOfPlanes)
        {
            if (plane.name == "Plane 1")
            {
                Debug.Log("Plane 1");
                plane.define_plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0.05f));
            }
            if(plane.name == "Plane 2")
            {
                Debug.Log("plane 2");
                plane.define_plane(new Vector3(0, -2, 10), new Vector3(0, 1, -45f));
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void define_plane(Vector3 point, Vector3 normal)
    {
        point_on_plane = point;
        normal_to_plane = normal;
        transform.position = point_on_plane;

        transform.up = normal_to_plane;
    }

    public float distance_to(Vector3 point)
    {
        Vector3 point_on_plane_to_point = point - point_on_plane;

        return Vector3.Dot(point_on_plane_to_point, normal_to_plane);
    }
}