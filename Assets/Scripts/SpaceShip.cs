using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Vuforia;

public class SpaceShip : MonoBehaviour
{
    Rigidbody rb;
    GameObject[] Planets; 
    const float G = 100f;
    float liftOffTimer = 0;
    public bool isGrounded = true;
    [SerializeField]
    float rotSpeed = 80f;
    float speed = 50;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }

   private void FixedUpdate()
    {
        if (isGrounded)
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
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            rb.drag = 0;
            rb.angularDrag = 5;
            isGrounded = true;
            rb.velocity = collision.rigidbody.velocity;
        }
    }

    private void Update()
    {
        if(GameManager.stateOfPlayer == 2)
        {
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                liftOffTimer += Time.deltaTime;
                if (liftOffTimer >= 1)
                {
                    liftOffTimer = 0;
                    transform.position += transform.up * 0.5f;
                    GetComponent<Rigidbody>().AddForce(transform.up * 0.02f, ForceMode.Impulse);
                    GameManager.stateOfPlayer = 2;
                    isGrounded = false;
                }
            }
            else
            {
                if (!isGrounded)
                {
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        GameManager.stateOfPlayer = 3;
                    }
                }
            }
        }
        if(GameManager.stateOfPlayer == 3)
        {
            if (isGrounded)
            {
                GameManager.stateOfPlayer = 2;
            }
            rb.drag = 0.1f;
            rb.angularDrag = 5;
            Thrust();
            Turn();
        }
    }

    void Turn()
    {
        float yaw = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        float pitch = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime * 0.3f;
        float roll = Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime * 0.3f;
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(-pitch, yaw, roll), 40f * Time.deltaTime);
    }

    void Thrust()
    {
        float thrustX = Input.GetAxis("Vertical") * speed * Time.deltaTime * 30;
        float thrustY = Input.GetAxis("Float") * speed * Time.deltaTime * 5;
        rb.AddRelativeForce(new Vector3(thrustX, thrustY, 0), ForceMode.Acceleration);
    }
}
