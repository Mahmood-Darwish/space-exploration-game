using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    float horozantialMovement;
    float verticalMovement;
    float speed = 10f;
    float jumpHeight = 0.0001f;
    GameObject[] Planets;
    const float G = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
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
            print(dirBetweenPlanets.normalized * force);
            if (force > curForce)
            {
                curForce = force;
                bigPlanet = dirBetweenPlanets;
            }
        }
        transform.rotation = Quaternion.FromToRotation(transform.up, -bigPlanet.normalized) * transform.rotation;
    }

    private void Update()
    {
        horozantialMovement = Input.GetAxis("Horizontal") * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime;
        Vector3 movement = (transform.forward * verticalMovement * speed) + (transform.right * horozantialMovement * speed);
        transform.position += movement;
    }
}
