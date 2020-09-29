using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class CollissionSystem : MonoBehaviour
    {
        PlaneScript plane;
        public Vector3 velocity;
        Vector3 acceleration = new Vector3(0, -9.8f, 0);
        public Rigidbody sphere;
        private float Radius_Of_Sphere = 0.5f;
        private float Coefficient_of_Restitution = 0.6f;
        private void Start()
        {
            plane = FindObjectOfType<PlaneScript>();
        }
        void OnCollisionEnter(Collision col)
        {
            Debug.Log("Collision Detected!");
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("triggered");

        }
        void Update()
        {
            float d1 = plane.distance_to(transform.position) - Radius_Of_Sphere;
            velocity += acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            float distance_from_center_to_plane = plane.distance_to(transform.position);
            float d2 = distance_from_center_to_plane - Radius_Of_Sphere;

            if (d2 <= 0)
            {
                Vector3 parallel = parallel_component(velocity, plane.normal);
                Vector3 perp = perpendicular_component(velocity, plane.normal);

                float time_of_impact = Time.deltaTime * d1 / (d1 - d2);
                transform.position -= velocity * (Time.deltaTime - time_of_impact);

                velocity = perp - Coefficient_of_Restitution * parallel;

                transform.position += velocity * (Time.deltaTime - time_of_impact);
            }
        }

        private void FixedUpdate()
        {
            if (sphere.GetComponent<Rigidbody>() == null || sphere.GetComponent<Rigidbody>().isKinematic)
            {
                transform.Translate(velocity * Time.deltaTime);
            }
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
}
