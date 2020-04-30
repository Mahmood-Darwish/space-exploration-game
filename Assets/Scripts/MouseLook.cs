using UnityEngine;
using UnityEngine.UI;
public class MouseLook : MonoBehaviour
{
    //  Sensitivity of the mouse.
    [SerializeField]
    float Sensitivity = 15f;

    //  Which text to display when looking at the door of the space ship.
    [SerializeField]
    Text text;

    //  References to gameobjects we will use later. 
    GameObject spaceCamera;
    GameObject player;

    //  Calculation support for looking around.
    float xRotation = 0f;

    //  Look up and save spaceship camera then deactivate it.
    private void Awake()
    {
        spaceCamera = GameObject.FindWithTag("SpaceCamera");
        spaceCamera.SetActive(false);
    }

    //  Save reference to the player then lock and hide the cursor.
    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        //  Take inputs.
        float MouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * Sensitivity;
        //  Rotate the player around the Y axis when taking input from Mouse X.
        player.transform.Rotate(Vector3.up * MouseX);
        //  Rotate the player around the X axis by mius input from Mouse Y and clamp it to make sure the player can't look to his back.
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //  Check of there's a ray intersection from the camera to 0.1 unit infront of the player. The intersecion will happen with doors of 
        //  the spaceship to allow the player to enter it.
        int layerMask = 1 << 16;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 0.1f, layerMask))
        {
            text.enabled = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.stateOfPlayer = 2;
                text.enabled = false;
                spaceCamera.SetActive(true);
                transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            text.enabled = false;
        }
    }
}
