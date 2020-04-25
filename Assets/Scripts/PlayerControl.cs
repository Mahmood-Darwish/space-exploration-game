using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    float horozantialMovement;
    float verticalMovement;
    float speed = 2;
    float jumpHeight = 0.0001f;
    GameObject[] Planets;
    const float G = 100f;
    bool jump;

    private void Start()
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
    
    private void Update()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
        horozantialMovement = Input.GetAxis("Horizontal") * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime;
        Vector3 movement = (transform.forward * verticalMovement * speed) + (transform.right * horozantialMovement * speed);
        transform.position += movement;
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            rb.velocity = collision.rigidbody.velocity;
        }
    }
}
