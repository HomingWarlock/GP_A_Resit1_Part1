using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour
{
    public GameObject player_object;
    public PlayerControl player_script;
    public GameObject cam_point;

    private float pos_lerp;
    private float rot_lerp;

    public float cutscene_no;

    private void Start()
    {
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<PlayerControl>();
        cam_point = GameObject.Find("Player_CamPivot");

        pos_lerp = 1;
        rot_lerp = 1;

        cutscene_no = 0;
    }

    private void Update()
    {
        if (!player_script.cutscene_mode)
        {
            pos_lerp = 1;
            rot_lerp = 1;
        }
        else if (player_script.cutscene_mode)
        {
            pos_lerp = 2 * Time.deltaTime;
            rot_lerp = 2 * Time.deltaTime;
        }

        transform.position = Vector3.Lerp(transform.position, cam_point.transform.position, pos_lerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, cam_point.transform.rotation, rot_lerp);

        if (cutscene_no == 1)
        {
            StartCoroutine(Cutscene1Time());
            cutscene_no = 1.5f;
        }

        if (cutscene_no == 2)
        {
            StartCoroutine(Cutscene2Time());
            cutscene_no = 2.5f;
        }

        if (cutscene_no == 3)
        {
            StartCoroutine(CutscenesEnd());
            cutscene_no = 3.5f;
        }
    }

    private IEnumerator Cutscene1Time()
    {
        yield return new WaitForSeconds(2);
        cutscene_no = 2;
        cam_point = GameObject.Find("Cutscene_EndCamPivot");
    }

    private IEnumerator Cutscene2Time()
    {
        yield return new WaitForSeconds(2);
        cutscene_no = 3;
        cam_point = GameObject.Find("Player_CamPivot");
    }

    private IEnumerator CutscenesEnd()
    {
        yield return new WaitForSeconds(2);
        player_script.cutscene_mode = false;
    }
}
