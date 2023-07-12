using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerControl player_script;

    void Start()
    {
        player_script = GetComponentInParent<PlayerControl>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            player_script.grounded = true;

            if (player_script.collected_Jump)
            {
                player_script.extra_jump = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            player_script.grounded = false;
        }
    }
}