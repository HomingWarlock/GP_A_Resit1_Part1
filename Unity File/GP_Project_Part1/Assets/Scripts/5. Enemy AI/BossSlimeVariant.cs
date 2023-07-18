using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlimeVariant : EnemyAction
{
    protected override void Start()
    {
        health = 1000;
        attack_damage = 30;
    }
}
