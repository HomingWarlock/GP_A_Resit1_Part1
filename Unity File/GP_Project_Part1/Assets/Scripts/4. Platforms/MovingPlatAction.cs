using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatAction : MonoBehaviour
{
    private Transform PointA;
    private Transform PointB;
    private string Goal;

    private float pos_lerp;

    void Start()
    {
        PointA = GameObject.Find("Platform_Moving1_PointA").transform;
        PointB = GameObject.Find("Platform_Moving1_PointB").transform;
        Goal = "PlatformMoving1_PointB";

        pos_lerp = 0;
    }

    void Update()
    {
        if (Goal == "PlatformMoving1_PointA" && transform.position != PointA.position)
        {
            transform.position = Vector3.Lerp(transform.position, PointA.position, pos_lerp);
        }
        
        if (Goal == "PlatformMoving1_PointA" && transform.position == PointA.position)
        {
            Goal = "Waiting";
            StartCoroutine(PreparingForB());
        }
        
        if (Goal == "PlatformMoving1_PointB" && transform.position != PointB.position)
        {
            transform.position = Vector3.Lerp(transform.position, PointB.position, pos_lerp);
        }

        if (Goal == "PlatformMoving1_PointB" && transform.position == PointB.position)
        {
            Goal = "Waiting";
            StartCoroutine(PreparingForA());
        }

        if (Vector3.Distance(transform.position, PointA.position) < 0.1 && Goal == "PlatformMoving1_PointA")
        {
            pos_lerp = 1;
        }
        else if (Vector3.Distance(transform.position, PointA.position) > 0.1 && Goal == "PlatformMoving1_PointA")
        {
            pos_lerp = 0.15f * Time.deltaTime;
        }

        if (Vector3.Distance(transform.position, PointB.position) < 0.1 && Goal == "PlatformMoving1_PointB")
        {
            pos_lerp = 1;
        }
        else if (Vector3.Distance(transform.position, PointB.position) > 0.1 && Goal == "PlatformMoving1_PointB")
        {
            pos_lerp = 0.15f * Time.deltaTime;
        }
    }

    private IEnumerator PreparingForA()
    {
        yield return new WaitForSeconds(1);
        pos_lerp = 0;
        Goal = "PlatformMoving1_PointA";
    }

    private IEnumerator PreparingForB()
    {
        yield return new WaitForSeconds(1);
        pos_lerp = 0;
        Goal = "PlatformMoving1_PointB";
    }
}
