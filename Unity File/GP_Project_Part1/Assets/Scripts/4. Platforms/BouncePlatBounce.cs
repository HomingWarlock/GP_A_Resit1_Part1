using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatBounce : MonoBehaviour
{
    public PlayerControl player_script;

    void Start()
    {
        player_script = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            if (player_script.bouncy_value < 40)
            {
                player_script.bouncy_value += 10;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            player_script.grounded = false;
            player_script.is_jumping = true;
            player_script.rb.velocity = new Vector3(player_script.rb.velocity.x, (player_script.jump_speed) + player_script.bouncy_value, player_script.rb.velocity.z);
        }
    }
}