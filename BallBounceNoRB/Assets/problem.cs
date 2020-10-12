using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class problem : MonoBehaviour
{
    GameObject sphereplease,plane;
    int i = 1;
    int j = 1;
    Vector3 Position,rotation;
    Quaternion Rotation;
    Vector3 point_on_plane, normal_to_plane;
    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector3(0, 5, 0);
        while (i <= 2)
        {
            sphereplease = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            sphereplease.transform.position = Position;
            string ScriptName = "PhysicsControl";
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            sphereplease.AddComponent(MyScriptType);
            sphereplease.name = "sphere " + i;

            string ScriptName2 = "Sphere";
            System.Type MyScriptType2 = System.Type.GetType(ScriptName2 + ",Assembly-CSharp");
            sphereplease.AddComponent(MyScriptType2);
            sphereplease.tag = "Sphere";
                
            transform.position = Sphere.point_on_sphere;

            transform.up = Sphere.normal_to_sphere;
            Position = new Vector3(0, 10, 0);
            i++;
        }

        Position = new Vector3(-5, -5, 0);
        rotation = new Vector3(0, 1, 0.05f);
        Rotation = new Quaternion(0, 1, 0.05f,0);
        
        while (j <= 2)
        {
            plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = Position;
            plane.transform.rotation = Rotation;
            point_on_plane = Position;
            normal_to_plane = rotation;
            transform.position = point_on_plane;
            transform.up = normal_to_plane;

            string ScriptName = "Plane";
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            plane.AddComponent(MyScriptType);

            plane.tag = "Plane";

            Position = new Vector3(-5, -1, 5);
            //rotation = new Vector3(-30, 0, 0);
            Rotation = new Quaternion(0.3f,0,0,0);
            j++;
        }

    }


}
