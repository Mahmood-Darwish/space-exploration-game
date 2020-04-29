using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    float horozantialMovement;
    float verticalMovement;
    float speed = 1;
    float jumpHeight = 0.005f;
    GameObject[] Planets;
    const float G = 100f;
    bool jump;
    public bool canJump;
    bool crouch = false;
    Vector3 crouching = new Vector3(0.1f, 0.05f, 0.1f);
    Vector3 notCrouching = new Vector3(0.1f, 0.1f, 0.1f);

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
        if (jump && canJump && !crouch)
        {
            canJump = false;
            transform.position += transform.up * 0.3f;
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }
    
    private void Update()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
        horozantialMovement = Input.GetAxis("Horizontal") * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime;
        crouch = Input.GetKey(KeyCode.LeftControl);
        Vector3 movement = (transform.forward * verticalMovement * speed) + (transform.right * horozantialMovement * speed);
        transform.position += movement;
        if (crouch)
        {
            speed = 0.5f;
            transform.localScale = crouching;
        }
        else { speed = 1; transform.localScale = notCrouching; }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            rb.velocity = collision.rigidbody.velocity;
        }
    }
}
