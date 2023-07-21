using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAction : MonoBehaviour
{
    protected GameObject player_object;
    protected PlayerControl player_script;

    protected Rigidbody rb;
    protected Animator anim;

    protected Vector3 spawn_pos;
    protected GameObject healthbar;
    protected Image healthbar_health;
    protected Image healthbar_damage;

    protected float health;
    protected float max_health;
    protected float attack_damage;
    protected int zone_no;
    protected float jump_speed;
    protected float move_distance;

    protected bool single_hop;
    protected bool single_attack;
    protected bool single_action;
    protected float start_action_wait_time;
    protected float action_wait_time;
    
    protected bool attack_damage_check;
    protected bool single_damaged_check;
    protected bool healthbar_damage_delay;
    protected Vector3 look_dir;

    protected bool is_jumping;
    protected bool is_dying;
    protected bool grounded;

    protected Transform slime_spawn1;
    protected Transform slime_spawn2;
    protected bool is_new_spawn;

    protected virtual void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();

        rb = GetComponent<Rigidbody>();
        anim = transform.Find("ModelPivot").GetComponent<Animator>();

        spawn_pos = transform.position;
        healthbar = gameObject.transform.Find("HealthBarGroup").gameObject;
        healthbar_health = transform.Find("HealthBarGroup/HealthBarCanvas/HealthBarValue").GetComponent<Image>();
        healthbar_damage = transform.Find("HealthBarGroup/HealthBarCanvas/HealthBarDamage").GetComponent<Image>();

        zone_no = 0;
        jump_speed = 10000;
        move_distance = 5;

        single_hop = false;
        single_attack = false;
        single_action = false;
        attack_damage_check = false;
        single_damaged_check = false;
        healthbar_damage_delay = false;

        is_jumping = false;
        is_dying = false;
        grounded = false;

        is_new_spawn = true;
        StartCoroutine(NewSpawnTimer());
    }

    protected virtual void Update()
    {
        look_dir = (player_object.transform.position - transform.position).normalized;
        healthbar.transform.forward = new Vector3(look_dir.x, 0, look_dir.z);

        if (!player_script.cutscene_mode && !player_script.is_dying)
        {
            if (!is_dying)
            {
                if (transform.position.y < -10)
                {
                    is_dying = true;
                }

                if (!is_new_spawn)
                {
                    if (!healthbar_damage_delay)
                    {
                        healthbar_damage_delay = true;
                        StartCoroutine(HealthBarDamageDelay());

                        if (healthbar_health.fillAmount < healthbar_damage.fillAmount)
                        {
                            healthbar_damage.fillAmount -= 0.005f;
                        }
                        else if (healthbar_health.fillAmount == healthbar_damage.fillAmount)
                        {
                            healthbar_damage.fillAmount = healthbar_health.fillAmount;
                        }
                    }

                    if (player_script.enemy_zone_no == zone_no)
                    {
                        transform.forward = new Vector3(look_dir.x, 0, look_dir.z);

                        if (!single_action)
                        {
                            Actions();
                        }     
                    }
                }
            }
            else if (is_dying)
            {
                if (healthbar_health.fillAmount < healthbar_damage.fillAmount)
                {
                    healthbar_damage.fillAmount -= 0.005f;
                }
                else if (healthbar_health.fillAmount == healthbar_damage.fillAmount)
                {
                    healthbar_damage.fillAmount = healthbar_health.fillAmount;
                }
            }

            anim.SetBool("isDying", is_dying);
        }
    }

    protected virtual void Actions()
    {
        single_action = true;

        if (Vector3.Distance(transform.position, player_object.transform.position) > move_distance)
        {
            if (!single_hop && grounded)
            {
                single_hop = true;
                action_wait_time = start_action_wait_time;
                StartCoroutine(SingleHopDelay(action_wait_time));
                grounded = false;
                is_jumping = true;
                rb.AddForce(new Vector3(look_dir.x * jump_speed, jump_speed, look_dir.z * jump_speed));
            }
        }
        else if (Vector3.Distance(transform.position, player_object.transform.position) < move_distance)
        {
            if (!single_attack && grounded)
            {
                single_attack = true;
                attack_damage_check = true;
                action_wait_time = action_wait_time + 2;
                StartCoroutine(SingleAttackDelay(action_wait_time));
                rb.AddForce(new Vector3(look_dir.x * (jump_speed + 15000), 2000, look_dir.z * (jump_speed + 15000)));
            }
        }
    }

    public void SetGrounded(bool _grounded)
    {
        grounded = _grounded;
    }
    
    public void SetIsJumping(bool isjumping)
    {
        is_jumping = isjumping;
    }

    public float GetAttackDamage(float damage)
    {
        damage = damage + attack_damage;
        return damage;
    }

    public void SetSingleAttack(bool singleattack)
    {
        singleattack = single_attack;
    }

    public bool GetSingleAttack(bool singleattack)
    {
        singleattack = single_attack;
        return singleattack;
    }

    public void SetSingleAction(bool singleaction)
    {
        single_action = singleaction;
    }

    public bool GetAttackDamageCheck(bool attackdamagecheck)
    {
        attackdamagecheck = attack_damage_check;
        return attackdamagecheck;
    }

    public void SetAttackDamageCheck(bool attackdamagecheck)
    {
        attack_damage_check = attackdamagecheck;
    }

    public virtual void TakeDamage(float damage)
    {
        if (!is_dying)
        {
            health -= damage;
            healthbar_health.fillAmount = health / max_health;

            if (health <= 0)
            {
                is_dying = true;
            }
        }
    }

    public bool GetSingleDamagedCheck(bool singledamagedcheck)
    {
        singledamagedcheck = single_damaged_check;
        return singledamagedcheck;
    }

    public void SetSingleDamagedCheck(bool singledamagedcheck)
    {
        single_damaged_check = singledamagedcheck;
    }

    public bool GetSpawnInfo(bool newspawn)
    {
        newspawn = is_new_spawn;
        return newspawn;
    }

    public void SetZone(int zoneNo)
    {
        zone_no = zoneNo;
    }

    protected IEnumerator SingleHopDelay(float actionWaitTime)
    {
        yield return new WaitForSeconds(actionWaitTime);
        single_action = false;
        single_hop = false;
    }

    protected IEnumerator SingleAttackDelay(float actionWaitTime)
    {
        yield return new WaitForSeconds(actionWaitTime);
        single_action = false;
        single_attack = false;
        attack_damage_check = false;
    }

    public IEnumerator SingleDamagedDelay()
    {
        yield return new WaitForSeconds(0.5f);
        single_damaged_check = false;
    }

    protected IEnumerator NewSpawnTimer()
    {
        yield return new WaitForSeconds(0.5f);
        is_new_spawn = false;
    }

    protected IEnumerator HealthBarDamageDelay()
    {
        yield return new WaitForSeconds(0.01f);
        healthbar_damage_delay = false;
    }
}
