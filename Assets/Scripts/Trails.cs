using UnityEngine;


/// <summary>
/// Leave a trail if the speed of the ship is higher than a vertian value.
/// </summary>
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
