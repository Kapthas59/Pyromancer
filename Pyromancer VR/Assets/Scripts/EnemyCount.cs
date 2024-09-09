using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCount : MonoBehaviour
{
    public bool canGoNextLevel = true;

    // Update is called once per frame
    void Update()
    {
        int tempCount = 0;
        foreach(Transform child in transform){
            if (child.gameObject.activeSelf) {
                tempCount ++;
            }
        }
        if (tempCount <= 0 && canGoNextLevel) {
            SceneManager.LoadScene(("VictoriousMenu"));
        }
    }
}
