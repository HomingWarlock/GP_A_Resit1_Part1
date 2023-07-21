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
                bool singleattack = enemy_script.GetSingleAttack(false);
                bool attackdamagecheck = enemy_script.GetAttackDamageCheck(false);
                float attackdamage = enemy_script.GetAttackDamage(0);

                if (attackdamagecheck)
                {
                    enemy_script.SetAttackDamageCheck(false);
                    col.GetComponent<PlayerControl>().TakeDamage(attackdamage);
                    col.GetComponent<PlayerControl>().is_hurting = true;
                }
            }
        }
    }
}
