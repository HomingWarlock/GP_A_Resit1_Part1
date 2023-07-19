using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOrbit : MonoBehaviour
{
    public GameObject player_object;
    private GameObject player_model;
    public PlayerControl player_script;

    void Start()
    {
        player_object = GameObject.Find("Player");
        player_model = GameObject.Find("PlayerModel");
        player_script = player_object.GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (!player_script.cutscene_mode) 
        {
            transform.RotateAround(player_object.transform.position, Vector3.up, Input.GetAxisRaw("Mouse X") * 1000 * Time.deltaTime);
            player_model.transform.forward = new Vector3(player_script.last_dir.x, 0, player_script.last_dir.z);
        }
    }
}