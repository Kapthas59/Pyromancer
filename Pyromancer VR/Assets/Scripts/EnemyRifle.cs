using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRifle : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectile;
    public float fireDelay = 1;

    public IEnumerator Shoot()
    {

        GetComponent<Animator>().GetBehaviour<BasicAttackState>().isShooting = true;
        
        var darkBall = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        darkBall.GetComponent<Rigidbody>().velocity = spawnPoint.forward * darkBall.GetComponent<FireBall>().ballSpeed;
        
        yield return new WaitForSeconds(fireDelay);
        GetComponent<Animator>().GetBehaviour<BasicAttackState>().isShooting = false;
    }
}
