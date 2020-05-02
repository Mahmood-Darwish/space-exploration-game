using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    //  Properties of the spaceship.
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Text text;
    [SerializeField]
    float rotSpeed = 80f;
    [SerializeField]
    float speed = 50;
    [SerializeField]
    float thrustPowerX = 30;
    [SerializeField]
    float thrustPowerY = 5;

    //  Support for our calculations.
    public bool isGrounded = true;
    float liftOffTimer = 0;
    float thrustX;
    float thrustY;
    Vector3 dirBetweenPlanets;
    float dis;
    float force;
    float curForce;
    public Vector3 bigPlanet;
    float yaw;
    float pitch;
    float roll;
    public static bool liftOff = false;

    //  Reference for later use.
    GameObject[] Planets; 
    public Rigidbody rb;

    //  Popluate the refrences.
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        Planets = GameObject.FindGameObjectsWithTag("Planet"); 
        text.enabled = false;
    }

    //  If the ship is grounded apply Newton's gravitational law and align the Y axis with forces vector of the planet with the largest
    //  gravitational pull. Also thrust if there's input.
   private void FixedUpdate()
    {
        if (isGrounded)
        {
            curForce = 0;
            bigPlanet = Vector3.zero;
            for (int i = 0; i < Planets.Length; i++)
            {
                dirBetweenPlanets = Planets[i].GetComponent<Rigidbody>().position - rb.position;
                dis = dirBetweenPlanets.magnitude;
                dis *= dis;
                force = GameManager.G * rb.mass * Planets[i].GetComponent<Rigidbody>().mass / dis;
                rb.AddForce(dirBetweenPlanets.normalized * force);
                if (force > curForce)
                {
                    curForce = force;
                    bigPlanet = dirBetweenPlanets;
                }
            }
            transform.rotation = Quaternion.FromToRotation(transform.up, -bigPlanet.normalized) * transform.rotation;
        }
        if(GameManager.stateOfPlayer == 3)
        {
            Thrust();
        }
    }

    //  While on the planet put player in state 2 and give the velocity of the planet to the ship.
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            if(GameManager.stateOfPlayer == 2)
                text.enabled = true;
            rb.drag = 0;
            rb.angularDrag = 5;
            isGrounded = true;
            rb.velocity = collision.rigidbody.velocity;
        }
    }

    //  When exiting the planet change the state of the player to 2 and remove the text saying you can leave the ship.
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            if (GameManager.stateOfPlayer == 2)
                text.enabled = false;
        }
    }

    //  When landing on a planet play an animation of opening doors.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            anim.SetBool("IsGrounded", true);
            anim.Play("Open Doors", 0, 1);
        }
    }

    private void Update()
    {
        //  If the ship is grounded.
        if(GameManager.stateOfPlayer == 2)
        {
            //  Get the player out of the ship and change cameras.
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.stateOfPlayer = 1;
                player.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").gameObject.transform.position;
                player.GetComponent<Rigidbody>().velocity = -player.GetComponent<Rigidbody>().transform.up;
                player.SetActive(true);
                if(GameObject.FindGameObjectWithTag("SpaceCamera") != null)
                  GameObject.FindGameObjectWithTag("SpaceCamera").SetActive(false);
                if (GameObject.FindGameObjectWithTag("FPSCamera") != null)
                    GameObject.FindGameObjectWithTag("FPSCamera").SetActive(false);
            }
            //  Do countdown for liftoff.
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                liftOffTimer += Time.deltaTime;
                liftOff = true;
                if (liftOffTimer >= 8.5)
                {
                    liftOff = false;
                    liftOffTimer = 0;
                    transform.position += transform.up * 0.5f;
                    GetComponent<Rigidbody>().AddForce(transform.up * 0.02f, ForceMode.Impulse);
                    GameManager.stateOfPlayer = 2;
                    anim.SetBool("IsGrounded", false);
                    anim.Play("Close Doors", 0, 0);
                    isGrounded = false;
                }
            }
            else
            {
                // When player lets go of space button change the state of the player.
                liftOffTimer = 0;
                liftOff = false;
                if (!isGrounded)
                {
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        GameManager.stateOfPlayer = 3;
                    }
                }
            }
        }
        //  If in third state, allow for controls. 
        if(GameManager.stateOfPlayer == 3)
        {
            if (isGrounded)
            {
                GameManager.stateOfPlayer = 2;
            }
            rb.drag = 0.1f;
            rb.angularDrag = 5;
            thrustX = Input.GetAxis("Vertical") * speed * Time.deltaTime * thrustPowerX;
            thrustY = Input.GetAxis("Float") * speed * Time.deltaTime * thrustPowerY;
            Turn();
        }
        if (GameManager.stateOfPlayer == 1)
            text.enabled = false;
    }

    //  A helper function to turn the ship.
    void Turn()
    {
        yaw = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        pitch = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime * 0.3f;
        roll = Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime * 0.3f;
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(-pitch, yaw, roll), 40f * Time.deltaTime);
    }

    //  A helper function to thrust the ship.
    void Thrust()
    {
        rb.AddRelativeForce(new Vector3(thrustX, thrustY, 0), ForceMode.Acceleration);
    }
}
