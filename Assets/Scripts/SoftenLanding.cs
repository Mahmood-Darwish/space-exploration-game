using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftenLanding : MonoBehaviour
{
    SpaceShip parentt;

    private void Start()
    {
        parentt = transform.parent.gameObject.GetComponent<SpaceShip>();
    }
    private void Update()
    {
        if (parentt.isGrounded)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(10, 90, 0), 0.0125f); 
        }
    }
}
