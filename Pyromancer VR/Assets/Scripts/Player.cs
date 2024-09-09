using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public HealthBar healthBar;

    public static int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level 1") {
            currentLevel = 1;
        }
        else if (sceneName == "Level 2") {
            currentLevel = 2;
        }
        else if (sceneName == "Level 3") {
            currentLevel = 3;
        }
        else if (sceneName == "StartMenu") {
            currentLevel = 0;
        }
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            defeated();
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }

    private void defeated()
    {
        SceneManager.LoadScene(("DefeatedMenu"));
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "EnemyBlade")
        {
            takeDamage(collider.gameObject.GetComponent<EnemySlice>().sliceDamage);
        }
        else if (collider.tag == "DarkBall")
        {
            takeDamage(collider.gameObject.GetComponent<FireBall>().ballDamage);
        }
    }
}
