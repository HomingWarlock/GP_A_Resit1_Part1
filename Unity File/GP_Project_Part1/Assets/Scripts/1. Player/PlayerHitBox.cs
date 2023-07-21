using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public GameObject player_object;
    public PlayerControl player_script;

    public EnemyAction enemy_script;

    private GameObject switch_button;
    private MeshRenderer button_rend;


    private GameObject door;

    void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();

        switch_button = GameObject.Find("Switch_Button");
        button_rend = switch_button.GetComponent<MeshRenderer>();
        button_rend.material.color = Color.red;

        door = GameObject.Find("Door");
    }

    void Update()
    {
        if (!player_script.cutscene_mode)
        {
            transform.RotateAround(player_object.transform.position, Vector3.up, Input.GetAxisRaw("Mouse X") * 1000 * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (player_script.attack_start_check)
        {
            if (col.gameObject.name == "DoorSwitch" && door != null)
            {
                button_rend.material.color = Color.green;
                door.GetComponent<DoorAction>().door_open = true;
                Destroy(door.GetComponent<DoorAction>().box_col, 2);
                Destroy(door, 2);
            }

            if (col.tag == "Enemy")
            {
                enemy_script = col.GetComponent<EnemyAction>();
                bool singledamagedcheck_result = enemy_script.GetSingleDamagedCheck(false);
                bool spawninfo_result = enemy_script.GetSpawnInfo(false);

                if (!singledamagedcheck_result && !spawninfo_result)
                {
                    enemy_script.SetSingleDamagedCheck(true);
                    StartCoroutine(enemy_script.SingleDamagedDelay());
                    enemy_script.TakeDamage(1);
                }
            }
        }
    }
}
