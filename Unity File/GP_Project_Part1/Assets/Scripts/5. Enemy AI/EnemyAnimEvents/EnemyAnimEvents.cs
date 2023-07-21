using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    public void OnDead_AnimationEnd()
    {
        Destroy(transform.parent.gameObject);
    }
}
