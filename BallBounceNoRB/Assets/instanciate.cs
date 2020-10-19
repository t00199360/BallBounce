using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instanciate : MonoBehaviour       
{
    GameObject sphereplease,plane;
    int i = 1;
    int j = 1;
    
    Vector3 Scale = new Vector3(1f, 1f, 1f);
    Vector3 Position,rotation;
    Quaternion Rotation;
    Vector3 point_on_plane, normal_to_plane;

    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector3(0, 5, 0);                                            //All comments with GO were meant to be in relation to a gameobject but this was revised to be of type Sphere/plane
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
            Scale = Scale * 1.5f;
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
            

            string ScriptName = "Plane";                                            //script to be added to the plane
            System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-CSharp");
            plane.AddComponent(MyScriptType);                                       //adds script
            plane.name = "Plane " + j;                                              //sets the planes name
            plane.tag = "Plane";                                                    //sets the planes tag
            j++;                                                                    //increases iterator for while loop
        }

    }


}
