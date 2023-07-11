using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOrbit : MonoBehaviour
{
    public GameObject player_object;

    void Start()
    {
        player_object = GameObject.Find("Player");
    }

    void Update()
    {
        transform.RotateAround(player_object.transform.position, Vector3.up, Input.GetAxisRaw("Mouse X") * 3000 * Time.deltaTime);
    }
}