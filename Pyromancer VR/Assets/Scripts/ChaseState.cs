using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float playerLoseDistance = 15f;
    public float agentChaseSpeed = 3.5f;
    public float attackDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = agentChaseSpeed;
        agent.stoppingDistance = attackDistance;

        AudioSource audioSource = animator.GetComponent<EnemyBase>().audioSource;
        audioSource.clip = animator.GetComponent<EnemyBase>().chaseSound;
        audioSource.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.enabled) 
        {
            animator.SetTrigger("gotDefeated");
        }
        
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > playerLoseDistance)
        {
            animator.SetBool("isChasing", false);
            agent.stoppingDistance = 0.1f;
        }
        if (distance < agent.stoppingDistance)
        {   
            
            animator.SetBool("isAttacking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.enabled) {
            agent.SetDestination(animator.transform.position);
        }
        AudioSource audioSource = animator.GetComponent<EnemyBase>().audioSource;
        audioSource.Stop();
    }
}
