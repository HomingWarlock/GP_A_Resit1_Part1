using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    private Animator anim;

    private float move_speed;
    private float run_speed;
    public float jump_speed;
    private float X_value;
    private float Z_value;
    private bool is_walking;
    private bool is_running;
    public bool is_jumping;
    public bool is_double_jumping;
    public bool is_attacking;
    public bool is_hurting;
    public bool is_dying;
    public bool is_respawned;
    public bool grounded;
    private bool jump_input_check;
    public bool attack_input_check;
    public bool attack_start_check;
    private Vector3 back_dir;
    private Vector3 right_dir;
    public Vector3 true_dir;
    public Vector3 last_dir;
    private GameObject cam_point;

    public int coins;
    public TMPro.TextMeshProUGUI coin_value;
    private float boosted_speed;
    public bool extra_jump;
    public bool collected_Speed;
    public bool collected_Jump;
    public GameObject speed_effect;
    public GameObject jump_effect;

    public bool cutscene_mode;

    public float bouncy_value;
    public bool on_ice;

    public int enemy_zone_no;
    private float max_player_health;
    private float player_health;
    private bool healthbar_damage_delay;
    private Image healthbar_health;
    private Image healthbar_damage;
    private Vector3 spawn_pos;
    public bool respawning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.Find("PlayerModel").GetComponent<Animator>();

        move_speed = 300;
        run_speed = 0;
        jump_speed = 10;
        X_value = 0;
        Z_value = 0;
        is_walking = false;
        is_running = false;
        is_jumping = false;
        is_double_jumping = false;
        is_attacking = false;
        is_hurting = false;
        is_dying = false;
        is_respawned = false;
        grounded = false;
        jump_input_check = false;
        attack_input_check = false;
        attack_start_check = false;
        cam_point = GameObject.Find("Player_CamPivot");

        coins = 0;
        coin_value = GameObject.Find("CoinValue").GetComponent<TMPro.TextMeshProUGUI>(); ;
        coin_value.text = coins.ToString();
        boosted_speed = 0;
        extra_jump = false;
        collected_Speed = false;
        collected_Jump = false;
        speed_effect = GameObject.Find("Speed Effect");
        speed_effect.SetActive(false);
        jump_effect = GameObject.Find("Jump Effect");
        jump_effect.SetActive(false);

        cutscene_mode = false;

        bouncy_value = 0;
        on_ice = false;

        enemy_zone_no = 0;
        max_player_health = 200;
        player_health = max_player_health;
        healthbar_damage_delay = false;
        healthbar_health = GameObject.Find("PlayerHealthBarValue").GetComponent<Image>();
        healthbar_damage = GameObject.Find("PlayerHealthBarDamage").GetComponent<Image>();
        healthbar_health.fillAmount = player_health / max_player_health;
        healthbar_damage.fillAmount = healthbar_health.fillAmount;
        spawn_pos = transform.position;
        respawning = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.X))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (respawning)
        {
            respawning = false;
            StartCoroutine(RespawnDelay());
        }

        if (transform.position.y < -10)
        {
            TakeDamage(20);
        }

        if (!cutscene_mode)
        {
            if (!is_hurting && !is_dying && !is_respawned)
            {
                back_dir = Input.GetAxisRaw("Vertical") * cam_point.transform.forward;
                right_dir = Input.GetAxisRaw("Horizontal") * cam_point.transform.right;
                true_dir = back_dir + right_dir;
                last_dir = cam_point.transform.forward;

                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    is_walking = true;
                }
                else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
                {
                    is_walking = false;
                }

                if (Input.GetAxisRaw("Horizontal") > 0.05f)
                {
                    X_value = 1;
                }
                else if (Input.GetAxisRaw("Horizontal") < -0.05f)
                {
                    X_value = -1;
                }
                else if (Input.GetAxisRaw("Horizontal") < 0.05f && Input.GetAxisRaw("Horizontal") > -0.05f)
                {
                    X_value = 0;
                }

                if (Input.GetAxisRaw("Vertical") > 0.05f)
                {
                    Z_value = 1;
                }
                else if (Input.GetAxisRaw("Vertical") < -0.05f)
                {
                    Z_value = -1;
                }
                else if (Input.GetAxisRaw("Vertical") < 0.05f && Input.GetAxisRaw("Vertical") > -0.05f)
                {
                    Z_value = 0;
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    is_running = true;
                    run_speed = 300;
                }
                else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && !Input.GetKey(KeyCode.JoystickButton3))
                {
                    is_running = false;
                    run_speed = 0;
                }

                if (collected_Speed)
                {
                    boosted_speed = 700;
                }
                else if (!collected_Speed)
                {
                    boosted_speed = 0;
                }

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) && !jump_input_check)
                {
                    jump_input_check = true;
                    StartCoroutine(JumpInputDelay());

                    if (collected_Jump)
                    {
                        if (!grounded && extra_jump)
                        {
                            extra_jump = false;
                            is_double_jumping = true;
                            rb.velocity = new Vector3(rb.velocity.x, jump_speed, rb.velocity.z);
                        }
                        else if (grounded)
                        {
                            grounded = false;
                            is_jumping = true;
                            rb.velocity = new Vector3(rb.velocity.x, jump_speed, rb.velocity.z);
                        }
                    }
                    else
                    {
                        if (grounded)
                        {
                            grounded = false;
                            is_jumping = true;
                            rb.velocity = new Vector3(rb.velocity.x, jump_speed, rb.velocity.z);
                        }
                    }
                }

                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2) && !attack_input_check && grounded && !is_attacking)
                {
                    attack_input_check = true;
                    StartCoroutine(AttackInputDelay());
                    is_attacking = true;
                }
            }

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

            anim.SetFloat("XValue", X_value);
            anim.SetFloat("ZValue", Z_value);
            anim.SetBool("isRunning", is_running);
            anim.SetBool("isJumping", is_jumping);
            anim.SetBool("isGrounded", grounded);
            anim.SetBool("isDoubleJumping", is_double_jumping);
            anim.SetBool("isAttacking", is_attacking);
            anim.SetBool("isHurting", is_hurting);
            anim.SetBool("isDying", is_dying);
            anim.SetBool("isRespawned", is_respawned);
        }
        else if (cutscene_mode)
        {
            X_value = 0;
            Z_value = 0;
            is_running = false;
            is_jumping = false;
            grounded = true;
            is_double_jumping = false;
            is_attacking = false;
            is_hurting = false;
            is_dying = false;
            is_respawned = false;

            if (collected_Jump)
            {
                extra_jump = true;
            }

            anim.SetFloat("XValue", X_value);
            anim.SetFloat("ZValue", Z_value);
            anim.SetBool("isRunning", is_running);
            anim.SetBool("isJumping", is_jumping);
            anim.SetBool("isGrounded", grounded);
            anim.SetBool("isDoubleJumping", is_double_jumping);
            anim.SetBool("isAttacking", is_attacking);
            anim.SetBool("IsHurting", is_hurting);
            anim.SetBool("isDying", is_dying);
            anim.SetBool("isRespawned", is_respawned);
        }
    }

    private void FixedUpdate()
    {
        if (!cutscene_mode)
        {
            if (!is_hurting && !is_dying && !is_respawned)
            {
                if (is_walking)
                {
                    rb.velocity = new Vector3(true_dir.x * (move_speed + run_speed + boosted_speed) * Time.deltaTime, rb.velocity.y, true_dir.z * (move_speed + run_speed + boosted_speed) * Time.deltaTime);
                }
                else if (!is_walking && !on_ice)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                }
                else if (!is_walking && on_ice)
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
                }
            }
        }
        else if (cutscene_mode)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!is_dying)
        {
            player_health -= damage;
            healthbar_health.fillAmount = player_health / max_player_health;

            if (transform.position.y < -10)
            {
                transform.position = spawn_pos;
                is_respawned = true;
                anim.SetBool("isRespawned", is_respawned);
            }

            if (player_health <= 0)
            {
                is_dying = true;
            }
        }
    }

    private IEnumerator JumpInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        jump_input_check = false;
    }

    private IEnumerator AttackInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        attack_input_check = false;
    }

    protected IEnumerator HealthBarDamageDelay()
    {
        yield return new WaitForSeconds(0.01f);
        healthbar_damage_delay = false;
    }

    protected IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = spawn_pos;
        player_health = max_player_health;
        healthbar_health.fillAmount = player_health / max_player_health;
        is_respawned = true;
    }
}
