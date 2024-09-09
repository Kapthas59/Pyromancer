using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackState : StateMachineBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    public float attackDistance;
    public bool isRanged = false;

    public bool isShooting = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        AudioSource audioSource = animator.GetComponent<EnemyBase>().audioSource;
        audioSource.clip = animator.GetComponent<EnemyBase>().attackSound;
        audioSource.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.enabled) 
        {
            animator.SetTrigger("gotDefeated");
        }

        animator.transform.LookAt(player);
        
        if (isRanged == true && isShooting == false) {
            animator.GetComponent<EnemyRifle>().spawnPoint.LookAt(player);
            animator.GetComponent<EnemyRifle>().StartCoroutine(animator.GetComponent<EnemyRifle>().Shoot());
        }
        
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > agent.stoppingDistance + 1f)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource audioSource = animator.GetComponent<EnemyBase>().audioSource;
        audioSource.Stop();
    }
}
