using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //  Properties relating to the player.
    [SerializeField]
    Vector3 crouching = new Vector3(0.1f, 0.05f, 0.1f);
    [SerializeField]
    Vector3 notCrouching = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float jumpHeight = 0.005f;

    //  References to other gameobjects or components.
    Rigidbody rb;
    GameObject[] Planets;

    //  Variable support for movement.
    float horozantialMovement;
    float verticalMovement;
    bool jump;
    public bool canJump;
    bool crouch;
    Vector3 dirBetweenPlanets;
    float dis;
    float force;
    float curForce;
    Vector3 bigPlanet;

    //  Save the references.
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }

    private void FixedUpdate()
    {
        //  Apply Newton's gravitational law and align the player's up axis with the force vector from the planet with the 
        //  strongest gravitational pull.
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
        //  Check if the player can jump and allow him to jump if there's jump input.
        if (jump && canJump && !crouch)
        {
            canJump = false;
            transform.position += transform.up * 0.3f;
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }
    
    private void Update()
    {
        //  Take input.
        jump = Input.GetKeyDown(KeyCode.Space);
        horozantialMovement = Input.GetAxis("Horizontal") * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime;
        crouch = Input.GetKey(KeyCode.LeftControl);
        //  Move the player.
        Vector3 movement = (transform.forward * verticalMovement * speed) + (transform.right * horozantialMovement * speed);
        transform.position += movement;
        //  Crouch the player by scaling him down and lower his speed.
        if (crouch)
        {
            speed = 0.5f;
            transform.localScale = crouching;
        }
        else { speed = 1; transform.localScale = notCrouching; }
    }

    //  If you touch a planet take his velocity as your velocity.
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            rb.velocity = collision.rigidbody.velocity;
        }
    }
}
