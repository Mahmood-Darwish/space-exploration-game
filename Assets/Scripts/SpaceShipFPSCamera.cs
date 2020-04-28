using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipFPSCamera : MonoBehaviour
{
    [SerializeField]
    GameObject SpaceCamera;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpaceCamera.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
