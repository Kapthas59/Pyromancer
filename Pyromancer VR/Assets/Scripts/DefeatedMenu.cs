using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatedMenu : MonoBehaviour
{
    public GameObject defeatedMenu;
    public GameObject helpMenu;

    void Start() {
    }

    public void restartButtonFunction() {
        Time.timeScale = 1;
        if (Player.currentLevel == 1) {
            SceneManager.LoadScene(("Level 1"));
        }
        else if (Player.currentLevel == 2) {
            SceneManager.LoadScene(("Level 2"));
        }
        else if (Player.currentLevel == 3) {
            SceneManager.LoadScene(("Level 3"));
        }
    }

    public void helpButtonFunction() {
        defeatedMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void mainMenuButtonFunction() {
        Time.timeScale = 1;
        SceneManager.LoadScene(("StartMenu"));
    }

    public void backButtonFunction() {
        defeatedMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
}
