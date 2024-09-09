using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletedMenu : MonoBehaviour
{
    void Start() {
    }

    public void backButtonFunction() {
        Time.timeScale = 1;
        SceneManager.LoadScene(("StartMenu"));
    }
}
