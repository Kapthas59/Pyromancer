using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float health = 100f;

    public AudioClip patrolSound;
    public AudioClip chaseSound;
    public AudioClip attackSound;
    public AudioSource audioSource;

    private Vector3 moveDirection;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;

    private float burnStatus = 0f;
    private Coroutine InFireStream;
    private Coroutine InFireWall;
    private bool isDefeated = false;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isDefeated)
        {
            isDefeated = true;
            agent.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            animator.SetTrigger("gotDefeated");
            StartCoroutine(GotDestroyed());
        }
        
    }

    public void GotHit(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("gotDamaged");
        }
    }

    public IEnumerator GotBurn(float fireDamage)
    {
        burnStatus += fireDamage;
        yield return new WaitForSeconds(1);
    }

    private IEnumerator GotDestroyed()
    {
        //gameController.GetComponent<GameController>().enemiesKilled += 1;
        yield return new WaitForSeconds(10);

        Destroy(gameObject);
    }

    public IEnumerator InFire(float damage, float rate)
    {
        GotHit(damage);
        yield return new WaitForSeconds(rate);
        InFireStream = StartCoroutine(InFire(damage, rate));
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "FireStream")
        {
            if (InFireStream == null) {
                //StopCoroutine(InFireStream);
                InFireStream = StartCoroutine(InFire(collider.gameObject.GetComponent<FireStream>().streamDamage, collider.gameObject.GetComponent<FireStream>().streamRate));
            }
            
        }
        else if (collider.tag == "FireWall")
        {
            if (InFireWall == null) {
                //StopCoroutine(InFireWall);
                InFireWall = StartCoroutine(InFire(collider.gameObject.GetComponent<FireWall>().wallDamage, collider.gameObject.GetComponent<FireWall>().wallRate));
            }
            
        }
        else if (collider.tag == "FireBall")
        {
            GotHit(collider.gameObject.GetComponent<FireBall>().ballDamage);
            collider.gameObject.GetComponent<FireBall>().StartCoroutine(collider.gameObject.GetComponent<FireBall>().Destroyed());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "FireStream")
        {
            if (InFireStream != null) {
                StopCoroutine(InFireStream);
                InFireStream = null;
            }
        }
        else if (collider.tag == "FireWall")
        {
            if (InFireWall != null) {
                StopCoroutine(InFireWall);
                InFireWall = null;
            }
        }
    }
}

