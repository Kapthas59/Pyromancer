using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "FireBall"))
        {
            collider.gameObject.GetComponent<FireBall>().StartCoroutine(collider.gameObject.GetComponent<FireBall>().Destroyed());
        }
    }
}
