using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGreenSlimeVariant : EnemyAction
{
    [SerializeField] protected GameObject medium_slime_object;

    protected override void Start()
    {
        base.Start();

        max_health = 3;
        health = max_health;
        healthbar_health.fillAmount = health / max_health;
        healthbar_damage.fillAmount = healthbar_health.fillAmount;
        attack_damage = 50;
        action_wait_time = 2;

        slime_spawn1 = transform.Find("Spawn_1");
        slime_spawn2 = transform.Find("Spawn_2");
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        healthbar_health.fillAmount = health / max_health;

        if (health <= 0)
        {
            GameObject medium_slime1 = Instantiate(medium_slime_object, new Vector3(slime_spawn1.position.x, slime_spawn1.position.y, slime_spawn1.position.z), Quaternion.identity) as GameObject;
            medium_slime1.transform.name = "Medium_GreenSlime";

            GameObject medium_slime2 = Instantiate(medium_slime_object, new Vector3(slime_spawn2.position.x, slime_spawn2.position.y, slime_spawn2.position.z), Quaternion.identity) as GameObject;
            medium_slime2.transform.name = "Medium_GreenSlime";

            Destroy(this.gameObject);
        }
    }

    protected IEnumerator SingleAttackDelay(float actionWaitTime)
    {
        yield return new WaitForSeconds(actionWaitTime);
        single_attack = false;
        attack_damage_check = false;
    }
}
