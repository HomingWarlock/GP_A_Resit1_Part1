using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAction : MonoBehaviour
{
    protected GameObject player_object;
    protected PlayerControl player_script;

    protected Rigidbody rb;
    protected Vector3 spawn_pos;
    protected GameObject healthbar;
    protected Image healthbar_health;
    protected Image healthbar_damage;

    protected float health;
    protected float max_health;
    public float attack_damage;
    protected int zone_no;
    protected float jump_speed;

    protected bool single_hop;
    public bool single_attack;
    public float action_wait_time;
    public bool attack_damage_check;
    public bool single_damaged_check;
    protected bool healthbar_damage_delay;
    protected Vector3 look_dir;

    protected Transform slime_spawn1;
    protected Transform slime_spawn2;
    public bool is_new_spawn;

    protected virtual void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();

        rb = GetComponent<Rigidbody>();
        spawn_pos = transform.position;
        healthbar = gameObject.transform.Find("HealthBarGroup").gameObject;
        healthbar_health = transform.Find("HealthBarGroup/HealthBarCanvas/HealthBarValue").GetComponent<Image>();
        healthbar_damage = transform.Find("HealthBarGroup/HealthBarCanvas/HealthBarDamage").GetComponent<Image>();

        zone_no = 0;
        jump_speed = 10000;

        single_hop = false;
        single_attack = false;
        attack_damage_check = false;
        single_damaged_check = false;
        healthbar_damage_delay = false;

        is_new_spawn = true;
        StartCoroutine(NewSpawnTimer());
    }

    protected virtual void Update()
    {
        if (!player_script.is_dying)
        {

            if (transform.position.y < -10)
            {
                transform.position = spawn_pos;
            }

            look_dir = (player_object.transform.position - transform.position).normalized;
            healthbar.transform.forward = new Vector3(look_dir.x, 0, look_dir.z);

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

                    if (player_script.enemy_zone_no == zone_no)
                    {
                        transform.forward = new Vector3(look_dir.x, 0, look_dir.z);

                        Actions();
                    }
                }
            }
        }
    }

    protected virtual void Actions()
    {
        if (Vector3.Distance(transform.position, player_object.transform.position) > 6)
        {
            if (!single_hop)
            {
                single_hop = true;
                StartCoroutine(SingleHopDelay(action_wait_time));
                rb.AddForce(new Vector3(look_dir.x * jump_speed, jump_speed, look_dir.z * jump_speed));
            }
        }
        else if (Vector3.Distance(transform.position, player_object.transform.position) < 6)
        {
            if (!single_attack)
            {
                single_attack = true;
                StartCoroutine(SingleAttackDelay(action_wait_time));
                rb.AddForce(new Vector3(look_dir.x * (jump_speed + 5000), 1000, look_dir.z * (jump_speed + 5000)));
            }
        }
    }
    
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthbar_health.fillAmount = health / max_health;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void SetZone(int zoneNo)
    {
        zone_no = zoneNo;
    }

    protected IEnumerator SingleHopDelay(float actionWaitTime)
    {
        yield return new WaitForSeconds(actionWaitTime);
        single_hop = false;
    }

    protected IEnumerator SingleAttackDelay(float actionWaitTime)
    {
        yield return new WaitForSeconds(actionWaitTime);
        single_attack = false;
        attack_damage_check = false;
    }

    public IEnumerator SingleDamagedDelay()
    {
        yield return new WaitForSeconds(0.5f);
        single_damaged_check = false;
    }

    public IEnumerator NewSpawnTimer()
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
