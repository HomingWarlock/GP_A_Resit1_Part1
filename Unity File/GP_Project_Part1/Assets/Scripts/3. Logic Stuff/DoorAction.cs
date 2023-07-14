using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    public BoxCollider box_col;
    public bool door_open;

    void Start()
    {
        box_col = GetComponent<BoxCollider>();
        door_open = false;
    }

    void Update()
    {
        if (door_open)
        {
            transform.Translate(0, -10 * Time.deltaTime, 0);
        }
    }
}
