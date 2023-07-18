using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    protected GameObject player_object;
    protected PlayerControl player_script;

    protected Rigidbody rb;
    private Vector3 spawn_pos;

    protected int health;
    public int attack_damage;
    protected int zone_no;
    protected float jump_speed;

    protected bool single_hop;
    public bool single_attack;
    public bool single_damage_check;
    protected Vector3 jump_dir;

    protected bool slime_single_death;
    protected Transform slime_spawn1;
    protected Transform slime_spawn2;

    protected virtual void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();

        rb = GetComponent<Rigidbody>();
        spawn_pos = transform.position;

        health = 2;
        attack_damage = 5;
        zone_no = 0;
        jump_speed = 10000;

        single_hop = false;
        single_attack = false;
        single_damage_check = false;

        slime_single_death = false;
    }

    protected virtual void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = spawn_pos;
        }

        if (player_script.enemy_zone_no == zone_no)
        {
            jump_dir = (player_object.transform.position - transform.position).normalized;
            transform.forward = new Vector3(jump_dir.x, 0, jump_dir.z);

            if (Vector3.Distance(transform.position, player_object.transform.position) > 6)
            {
                if (!single_hop)
                {
                    single_hop = true;
                    StartCoroutine(SingleHopDelay());
                    rb.AddForce(new Vector3(jump_dir.x * jump_speed, jump_speed, jump_dir.z * jump_speed));
                }
            }
            else if (Vector3.Distance(transform.position, player_object.transform.position) < 6)
            {
                if (!single_attack)
                {
                    single_attack = true;
                    StartCoroutine(SingleAttackDelay());
                    rb.AddForce(new Vector3(jump_dir.x * (jump_speed + 5000), 1000, jump_dir.z * (jump_speed + 5000)));
                }
            }
        }
    }
    /*
    public GetDamage()
    {
        //
    }
    */
    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void SetZone(int zoneNo)
    {
        zone_no = zoneNo;
    }

    protected IEnumerator SingleHopDelay()
    {
        yield return new WaitForSeconds(1);
        single_hop = false;
    }

    protected IEnumerator SingleAttackDelay()
    {
        yield return new WaitForSeconds(1);
        single_attack = false;
        single_damage_check = false;
    }
}
