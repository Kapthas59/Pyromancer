using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject helpMenu;
    public PlayerFunction playerFunction;

    void Start() {
        playerFunction = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerFunction>();
    }

    public void resumeButtonFunction() {
        Time.timeScale = 1;
        playerFunction.XROrigin.position = new Vector3(playerFunction.unpausedPosition.x, playerFunction.unpausedPosition.y, playerFunction.unpausedPosition.z);
        playerFunction.transform.position = new Vector3(playerFunction.unpausedPosition.x, playerFunction.unpausedPosition.y, playerFunction.unpausedPosition.z);
        Destroy(playerFunction.menuAreaGameObject);
        playerFunction.menuAreaActive = false;
        
        playerFunction.leftControllerRay.enabled = false;
        playerFunction.rightControllerRay.enabled = false;
    }

    public void helpButtonFunction() {
        pauseMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void mainMenuButtonFunction() {
        Time.timeScale = 1;
        SceneManager.LoadScene(("StartMenu"));
    }

    public void backButtonFunction() {
        pauseMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
}
