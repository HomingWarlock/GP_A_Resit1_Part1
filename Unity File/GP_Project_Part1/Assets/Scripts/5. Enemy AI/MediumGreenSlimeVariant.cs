using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumGreenSlimeVariant : EnemyAction
{
    public GameObject small_slime_object;

    protected override void Start()
    {
        base.Start();

        health = 2;
        attack_damage = 5;

        slime_spawn1 = transform.Find("Spawn_1");
        slime_spawn2 = transform.Find("Spawn_2");
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && !slime_single_death)
        {
            slime_single_death = true;

            GameObject smaller_slime1 = Instantiate(small_slime_object, new Vector3(slime_spawn1.position.x, slime_spawn1.position.y, slime_spawn1.position.z), Quaternion.identity) as GameObject;
            smaller_slime1.transform.name = "Small_GreenSlime";

            GameObject smaller_slime2 = Instantiate(small_slime_object, new Vector3(slime_spawn2.position.x, slime_spawn2.position.y, slime_spawn2.position.z), Quaternion.identity) as GameObject;
            smaller_slime2.transform.name = "Small_GreenSlime";

            Destroy(this.gameObject);
        }
    }
}
