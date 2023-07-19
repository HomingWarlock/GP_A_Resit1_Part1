using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public PlayerControl player_script;

    void Start()
    {
        player_script = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    public void OnAttack_HitStart()
    {
        player_script.attack_start_check = true;
    }

    public void OnAttack_HitEnd()
    {
        player_script.attack_start_check = false;
    }

    public void OnAttack_AnimationEnd()
    {
        player_script.is_attacking = false;
    }

    public void OnHurt_AnimationEnd()
    {
        player_script.is_hurting = false;
    }

    public void OnDead_AnimationEnd()
    {
        if (player_script.is_dying)
        {
            player_script.is_dying = false;
            player_script.respawning = true;
        }
    }

    public void OnRespawn_AnimationEnd()
    {
        player_script.is_hurting = false;
        player_script.is_dying = false;
        player_script.is_respawned = false;
    }
}
