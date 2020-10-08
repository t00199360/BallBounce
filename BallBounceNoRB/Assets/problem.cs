using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class problem : MonoBehaviour
{
    GameObject sphereplease;
    int i = 0;
    Vector3 Position;
    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector3(0, 5, 0);
        while (i < 2)
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
        
    }


}
