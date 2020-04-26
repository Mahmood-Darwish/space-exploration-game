using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerControl player;
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<PlayerControl>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        player.canJump = true;
    }

}
