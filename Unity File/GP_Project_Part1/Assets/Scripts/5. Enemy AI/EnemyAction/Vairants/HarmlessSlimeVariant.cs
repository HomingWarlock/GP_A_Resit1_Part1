using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmlessSlimeVariant : EnemyAction
{
    protected override void Start()
    {
        base.Start();

        max_health = 2;
        health = max_health;
        healthbar_health.fillAmount = health / max_health;
        healthbar_damage.fillAmount = healthbar_health.fillAmount;
        attack_damage = 0;
        action_wait_time = 1.5f;
    }

    protected override void Actions()
    {
        single_action = true;

        if (Vector3.Distance(transform.position, player_object.transform.position) > move_distance)
        {
            if (!single_hop && grounded)
            {
                single_hop = true;
                StartCoroutine(SingleHopDelay(action_wait_time));
                grounded = false;
                is_jumping = true;
                rb.AddForce(new Vector3(look_dir.x * jump_speed, jump_speed, look_dir.z * jump_speed));
            }
        }
        else
        {
            single_action = false;
        }
    }
}
