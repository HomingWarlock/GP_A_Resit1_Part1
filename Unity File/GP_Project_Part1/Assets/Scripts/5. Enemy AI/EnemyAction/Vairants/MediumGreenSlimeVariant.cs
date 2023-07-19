using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumGreenSlimeVariant : EnemyAction
{
    [SerializeField] protected GameObject small_slime_object;

    protected override void Start()
    {
        base.Start();

        max_health = 2;
        health = max_health;
        healthbar_health.fillAmount = health / max_health;
        healthbar_damage.fillAmount = healthbar_health.fillAmount;
        attack_damage = 20;
        action_wait_time = 1;

        slime_spawn1 = transform.Find("Spawn_1");
        slime_spawn2 = transform.Find("Spawn_2");
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        healthbar_health.fillAmount = health / max_health;

        if (health <= 0)
        {
            GameObject smaller_slime1 = Instantiate(small_slime_object, new Vector3(slime_spawn1.position.x, slime_spawn1.position.y, slime_spawn1.position.z), Quaternion.identity) as GameObject;
            smaller_slime1.transform.name = "Small_GreenSlime";

            GameObject smaller_slime2 = Instantiate(small_slime_object, new Vector3(slime_spawn2.position.x, slime_spawn2.position.y, slime_spawn2.position.z), Quaternion.identity) as GameObject;
            smaller_slime2.transform.name = "Small_GreenSlime";

            Destroy(this.gameObject);
        }
    }
}
