using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAction : MonoBehaviour
{
    public PlayerControl player_script;

    private MeshRenderer rend;
    private BoxCollider box_col;

    private float rotate_speed;

    private float jb_timer;
    private float sb_timer;
    private IEnumerator jb_IEnum;
    private IEnumerator sb_IEnum;

    private float timer_value;

    void Start()
    {
        player_script = GameObject.Find("Player").GetComponent<PlayerControl>();

        rend = GetComponent<MeshRenderer>();
        box_col = GetComponent<BoxCollider>();

        rotate_speed = 50;
    }

    void Update()
    {
        transform.Rotate(0, rotate_speed * Time.deltaTime, 0);

        if (!player_script.cutscene_mode)
        {
            timer_value = Time.deltaTime;
        }
        else if (player_script.cutscene_mode)
        {
            timer_value = 0;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            if (this.gameObject.name == "Yellow Coin")
            {
                player_script.coins += 1;
                player_script.coin_value.text = player_script.coins.ToString();
                rend.enabled = false;
                box_col.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Red Coin")
            {
                player_script.coins += 5;
                player_script.coin_value.text = player_script.coins.ToString();
                rend.enabled = false;
                box_col.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Blue Coin")
            {
                player_script.coins += 10;
                player_script.coin_value.text = player_script.coins.ToString();
                rend.enabled = false;
                box_col.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Diamond")
            {
                player_script.coins += 50;
                player_script.coin_value.text = player_script.coins.ToString();
                rend.enabled = false;
                box_col.enabled = false;
                StartCoroutine(Respawn_Delay());
            }

            if (this.gameObject.name == "Speed Boost")
            {
                player_script.collected_Speed = true;
                player_script.speed_effect.SetActive(true);
                rend.enabled = false;
                box_col.enabled = false;
                StartCoroutine(Respawn_Delay());
                sb_timer += 5;

                if (sb_IEnum != null)
                {
                    StopCoroutine(sb_IEnum);
                }

                sb_IEnum = SpeedBoostTimer();
                StartCoroutine(sb_IEnum);
            }

            if (this.gameObject.name == "Jump Boost")
            {
                player_script.collected_Jump = true;
                player_script.jump_effect.SetActive(true);
                player_script.extra_jump = true;
                rend.enabled = false;
                box_col.enabled = false;
                StartCoroutine(Respawn_Delay());
                jb_timer += 5;

                if (jb_IEnum != null)
                {
                    StopCoroutine(jb_IEnum);
                }

                jb_IEnum = JumpBoostTimer();
                StartCoroutine(jb_IEnum);
            }
        }
    }

    private IEnumerator SpeedBoostTimer()
    {
        if (!player_script.cutscene_mode)
        {
            for (; sb_timer > 0; sb_timer -= timer_value)
            {
                yield return null;
            }
            player_script.collected_Speed = false;
            player_script.speed_effect.SetActive(false);
        }
    }

    private IEnumerator JumpBoostTimer()
    {
        if (!player_script.cutscene_mode)
        {
            for (; jb_timer > 0; jb_timer -= timer_value)
            {
                yield return null;
            }
            player_script.collected_Jump = false;
            player_script.jump_effect.SetActive(false);
            player_script.extra_jump = false;
        }
    }

    private IEnumerator Respawn_Delay()
    {
        yield return new WaitForSeconds(1);
        rend.enabled = true;
        box_col.enabled = true;
    }
}
