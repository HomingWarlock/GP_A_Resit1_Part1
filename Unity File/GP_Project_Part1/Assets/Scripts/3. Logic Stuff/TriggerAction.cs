using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public GameObject player_object;
    public PlayerControl player_script;

    private float rotate_speed;

    void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();

        rotate_speed = 50;
    }

    void Update()
    {
        transform.Rotate(0, rotate_speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject.GetComponent<BoxCollider>());
            player_object.transform.position = new Vector3(this.transform.position.x, -1, this.transform.position.z);
            player_script.cutscene_mode = true;
            Destroy(this.gameObject,0.5f);
        }
    }
}