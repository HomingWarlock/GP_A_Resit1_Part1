using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGreenSlimeVariant : EnemyAction
{
    protected override void Start()
    {
        base.Start();

        max_health = 1;
        health = max_health;
        healthbar_health.fillAmount = health / max_health;
        healthbar_damage.fillAmount = healthbar_health.fillAmount;
        attack_damage = 10;
        start_action_wait_time = 1;
        action_wait_time = start_action_wait_time;
    }
}
