using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatSlide : MonoBehaviour
{
    public PlayerControl player_script;
    
    void Start()
    {
        player_script = GameObject.Find("Player").GetComponent<PlayerControl>();

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            player_script.on_ice = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            player_script.on_ice = false;
        }
    }
}
