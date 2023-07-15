using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatMagnet : MonoBehaviour
{
    private GameObject MovingPlat;

    void Start()
    {
        MovingPlat = GameObject.Find("Ground_PlatformMoving1");
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.transform.SetParent(MovingPlat.transform, true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.transform.SetParent(null, true);
        }
    }
}
