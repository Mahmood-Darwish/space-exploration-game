using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Check if the player standing on anything. If yes he can jump.
/// </summary>
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
