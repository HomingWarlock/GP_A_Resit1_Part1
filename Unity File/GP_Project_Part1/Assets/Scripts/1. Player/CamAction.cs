using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour
{
    private GameObject cam_point;
    private float pos_lerp;
    private float rot_lerp;

    private void Start()
    {
        cam_point = GameObject.Find("CamPoint");
        pos_lerp = 1;
        rot_lerp = 1;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cam_point.transform.position, pos_lerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, cam_point.transform.rotation, rot_lerp);
    }
}
