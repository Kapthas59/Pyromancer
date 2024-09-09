using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriousMenu : MonoBehaviour
{
    public GameObject victoriousMenu;
    public GameObject helpMenu;

    void Start() {
    }

    public void continueButtonFunction() {
        Time.timeScale = 1;
        if (Player.currentLevel == 1) {
            SceneManager.LoadScene(("Level 2"));
        }
        else if (Player.currentLevel == 2) {
            SceneManager.LoadScene(("Level 3"));
        }
        else if (Player.currentLevel == 3) {
            SceneManager.LoadScene(("GameComplete"));
        }
    }

    public void helpButtonFunction() {
        victoriousMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void mainMenuButtonFunction() {
        Time.timeScale = 1;
        SceneManager.LoadScene(("StartMenu"));
    }

    public void backButtonFunction() {
        victoriousMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
}
