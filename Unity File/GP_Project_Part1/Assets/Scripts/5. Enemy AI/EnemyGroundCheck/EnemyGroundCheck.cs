using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    protected EnemyAction enemy_script;

    void Start()
    {
        enemy_script = GetComponentInParent<EnemyAction>();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Ground")
        {
            enemy_script.SetGrounded(true);
            enemy_script.SetIsJumping(false);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ground")
        {
            enemy_script.SetGrounded(false);
        }
    }
}
