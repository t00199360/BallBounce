using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class problem : MonoBehaviour
{
    GameObject sphereplease;

    // Start is called before the first frame update
    void Start()
    {
        sphereplease = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        string ScriptName = "PhysicsControl";
        System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
        sphereplease.AddComponent(MyScriptType);

        string ScriptName2 = "Sphere";
        System.Type MyScriptType2 = System.Type.GetType(ScriptName2 + ",Assembly-CSharp");
        sphereplease.AddComponent(MyScriptType2);
        sphereplease.tag = "Sphere";

        transform.position = Sphere.point_on_sphere;

        transform.up = Sphere.normal_to_sphere;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
