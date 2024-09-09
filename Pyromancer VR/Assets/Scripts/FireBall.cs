using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float ballLife = 5f;
    public float ballDamage = 10f;
    public float ballSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(ballLife);
        StartCoroutine(Destroyed());
    }

    public IEnumerator Destroyed()
    {
        gameObject.GetComponent<AudioSource>().Play();
        if (gameObject.transform.childCount > 0) {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        StartCoroutine(Destroyed());
    }
}
