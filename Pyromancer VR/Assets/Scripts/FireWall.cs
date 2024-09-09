using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public float wallLife = 5f;
    public float wallDamage = 0.1f;
    public float wallRate = 0.1f;

    private Transform XROrigin;

    // Start is called before the first frame update
    void Start()
    {
        XROrigin = GameObject.FindWithTag("XROrigin").transform;
        transform.position = new Vector3(transform.position.x, XROrigin.position.y, transform.position.z);
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(wallLife);
        Destroy(gameObject);
    }
}
