using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGreenSlimeVariant : EnemyAction
{
    public GameObject medium_slime_object;

    protected override void Start()
    {
        base.Start();

        health = 3;
        attack_damage = 10;

        slime_spawn1 = transform.Find("Spawn_1");
        slime_spawn2 = transform.Find("Spawn_2");
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && !slime_single_death)
        {
            slime_single_death = true;

            GameObject medium_slime1 = Instantiate(medium_slime_object, new Vector3(slime_spawn1.position.x, slime_spawn1.position.y, slime_spawn1.position.z), Quaternion.identity) as GameObject;
            medium_slime1.transform.name = "Medium_GreenSlime";

            GameObject medium_slime2 = Instantiate(medium_slime_object, new Vector3(slime_spawn2.position.x, slime_spawn2.position.y, slime_spawn2.position.z), Quaternion.identity) as GameObject;
            medium_slime2.transform.name = "Medium_GreenSlime";

            Destroy(this.gameObject);
        }
    }
}
