using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics_Script : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -9.8f, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if ((transform.position.y - 0.5f) < 0f)
        {
            transform.position -= velocity * Time.deltaTime;
            velocity = -velocity * 0.5f;

        }
    }
}
