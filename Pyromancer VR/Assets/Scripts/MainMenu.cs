using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject levelMenu;
    public GameObject helpMenu;

    public void playButtonFunction() {
        startMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
    public void helpButtonFunction() {
        startMenu.SetActive(false);
        helpMenu.SetActive(true);
    }
    public void exitButtonFunction() {
        Application.Quit();
    }
    public void levelButtonFunction(int levelNum) {
        if (levelNum == 1) {
            SceneManager.LoadScene(("Level 1"));
        }
        else if (levelNum == 2) {
            SceneManager.LoadScene(("Level 2"));
        }
        else if (levelNum == 3) {
            SceneManager.LoadScene(("Level 3"));
        }
    }
    public void backButtonFunction() {
        startMenu.SetActive(true);
        levelMenu.SetActive(false);
        helpMenu.SetActive(false);
    }
}
