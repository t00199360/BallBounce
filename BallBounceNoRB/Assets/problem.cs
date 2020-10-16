using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class problem : MonoBehaviour        //this class needs renaming from problem to object instanciation also probably needs better partitioning to seperate logic
{
    GameObject sphereplease,plane;
    int i = 1;
    int j = 1;
    float Mass = 1f;
    Vector3 Scale = new Vector3(1f, 1f, 1f);
    Vector3 Position,rotation;
    Quaternion Rotation;
    Vector3 point_on_plane, normal_to_plane;

    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector3(0, 5, 0);
        while (i <= 2)
        {
            sphereplease = GameObject.CreatePrimitive(PrimitiveType.Sphere);        //creates first sphere
            
            sphereplease.transform.position = Position;                             //sets position
            sphereplease.name = "sphere " + i;                                      //renames GO

            string ScriptName = "Sphere";                                          //script to be added to the GO
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            sphereplease.AddComponent(MyScriptType);                               //adds second script

            sphereplease.tag = "Sphere";                                            //tags sphere as "Sphere"
            sphereplease.transform.localScale = Scale;
            Scale = new Vector3(1,1,1);
            Position = new Vector3(0, 10, 0);                                       //changes the global 'Position' in prep for the second sphere instanciation
            i++;                                                                    //increases iterator for while loop
        }

        Position = new Vector3(0, -5, 0);                                           //resetting position and rotation values for Plane instanciation
        rotation = new Vector3(0, 1, 0.05f);
        Rotation = new Quaternion(0, 1, 0.05f,0);
        
        while (j <= 2)      //this belongs in plane.cs but if i apply it to each plane, therell be infinite numbers of planes being instanciated
        {
            plane = GameObject.CreatePrimitive(PrimitiveType.Plane);                //creates plane
            plane.transform.position = Position;                                    //sets position
            plane.transform.rotation = Rotation;                                    //sets rotation
            point_on_plane = Position;                                              //sets point_on_plane
            normal_to_plane = rotation;                                             //sets the planes normal
            transform.position = point_on_plane;                                    //sets the planes position
            transform.up = normal_to_plane;                                         //sets the .up of the plane

            string ScriptName = "Plane";                                            //script to be added to the plane
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            plane.AddComponent(MyScriptType);                                       //adds script
            plane.name = "Plane " + j;                                              //sets the planes name
            plane.tag = "Plane";                                                    //sets the planes tag

            Position = new Vector3(-5, -1, 5);                                      //sets position for next plane
            rotation = new Vector3(-90, 0, 0);                                      //sets rotation for next plane
            j++;                                                                    //increases iterator for while loop
        }

    }


}
