using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    private GameObject player_object;
    private PlayerControl player_script;

    [SerializeField] private string zone_name;
    [SerializeField] private int zone_no;

    void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();

        zone_name = transform.name;
        zone_no = int.Parse(zone_name.Substring(zone_name.Length - 1));
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            player_script.enemy_zone_no = zone_no;
        }

        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyAction>().SetZone(zone_no);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            player_script.enemy_zone_no = 0;
        }

        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyAction>().SetZone(0);
        }
    }
}
