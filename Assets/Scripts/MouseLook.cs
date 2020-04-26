using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    GameObject player;
    float Sensitivity = 10f;
    float xRotation = 0f;
    GameObject spaceCamera;
    [SerializeField]
    Text text;

    private void Awake()
    {
        spaceCamera = GameObject.FindWithTag("SpaceCamera");
        spaceCamera.SetActive(false);
    }

    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * Sensitivity;
        player.transform.Rotate(Vector3.up * MouseX);
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        int layerMask = 1 << 16;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 0.1f, layerMask))
        {
            text.enabled = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                spaceCamera.SetActive(true);
                transform.parent.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else
        {
            text.enabled = false;
        }
    }
}
