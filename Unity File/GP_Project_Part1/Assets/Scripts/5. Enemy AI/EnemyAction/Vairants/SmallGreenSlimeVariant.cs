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
        action_wait_time = 0.5f;
    }
}
