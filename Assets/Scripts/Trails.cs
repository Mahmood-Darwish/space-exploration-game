using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trails : MonoBehaviour
{
    SpaceShip parnett;
    void Start()
    {
        parnett = transform.parent.gameObject.GetComponent<SpaceShip>();
    }

    void Update()
    {
        if(!parnett.isGrounded && parnett.rb.velocity.magnitude > 45)
        {
            gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }
    }
}
