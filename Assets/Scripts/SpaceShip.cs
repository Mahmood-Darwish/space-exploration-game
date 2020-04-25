using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    Rigidbody rb;
    GameObject[] Planets; 
    const float G = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }

   private void FixedUpdate()
    {
        Vector3 dirBetweenPlanets;
        float dis;
        float force;
        float curForce = 0;
        Vector3 bigPlanet = Vector3.zero;
        for (int i = 0; i < Planets.Length; i++)
        {
            dirBetweenPlanets = Planets[i].GetComponent<Rigidbody>().position - rb.position;
            dis = dirBetweenPlanets.magnitude;
            dis *= dis;
            force = G * rb.mass * Planets[i].GetComponent<Rigidbody>().mass / dis;
            rb.AddForce(dirBetweenPlanets.normalized * force);
            if (force > curForce)
            {
                curForce = force;
                bigPlanet = dirBetweenPlanets;
            }
        }
        transform.rotation = Quaternion.FromToRotation(transform.up, -bigPlanet.normalized) * transform.rotation;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            rb.velocity = collision.rigidbody.velocity;
        }
    }
}
