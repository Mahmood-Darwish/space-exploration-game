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
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = new Quaternion(0, 0, 0, 1);
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }

   void FixedUpdate()
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
            if (force > curForce)
            {
                curForce = force;
                bigPlanet = dirBetweenPlanets;
            }
        }
        rb.AddRelativeForce(curForce * bigPlanet.normalized);
        print(bigPlanet);
    }
}
