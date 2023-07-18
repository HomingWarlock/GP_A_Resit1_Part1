using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGreenSlimeVariant : EnemyAction
{
    protected override void Start()
    {
        base.Start();

        health = 1;
        attack_damage = 1;
    }
}
