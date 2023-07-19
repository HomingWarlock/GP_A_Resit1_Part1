using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public EnemyAction enemy_script;

    void Start()
    {
        enemy_script = GetComponentInParent<EnemyAction>();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            if (!col.GetComponent<PlayerControl>().is_dying)
            {
                if (enemy_script.single_attack)
                {
                    if (!enemy_script.attack_damage_check)
                    {
                        enemy_script.attack_damage_check = true;
                        col.GetComponent<PlayerControl>().TakeDamage(enemy_script.attack_damage);
                        col.GetComponent<PlayerControl>().is_hurting = true;
                    }
                }
            }
        }
    }
}
