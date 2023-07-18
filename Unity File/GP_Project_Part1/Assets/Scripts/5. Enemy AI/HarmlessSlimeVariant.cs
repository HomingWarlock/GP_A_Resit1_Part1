using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmlessSlimeVariant : EnemyAction
{
    protected override void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();

        rb = GetComponent<Rigidbody>();

        health = 2;
        zone_no = 0;
        jump_speed = 10000;

        single_hop = false;
    }

    protected override void Update()
    {
        if (player_script.enemy_zone_no == zone_no && !single_hop)
        {
            single_hop = true;
            StartCoroutine(SingleHopDelay());
            jump_dir = (player_object.transform.position - transform.position).normalized;
            transform.forward = new Vector3(jump_dir.x, 0, jump_dir.z);
            rb.AddForce(new Vector3(jump_dir.x * jump_speed, jump_speed, jump_dir.z * jump_speed));
        }
    }
}
