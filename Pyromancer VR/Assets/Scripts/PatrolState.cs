using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    float timer;
    List<Transform> waypoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;

    public float chaseRange = 8;
    public float patrolRadius = 5f;
    public float patrolTime = 3f;
    public float agentWalkSpeed = 1.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = agentWalkSpeed;
        timer = 0;
        
        agent.SetDestination(RandomNavmeshLocation(patrolRadius));

        AudioSource audioSource = animator.GetComponent<EnemyBase>().audioSource;
        audioSource.clip = animator.GetComponent<EnemyBase>().patrolSound;
        audioSource.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.enabled) 
        {
            animator.SetTrigger("gotDefeated");
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavmeshLocation(patrolRadius));
        }
        timer += Time.deltaTime;        

        if (timer > patrolTime)
        {
            animator.SetBool("isPatrolling", false);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        AudioSource audioSource = animator.GetComponent<EnemyBase>().audioSource;
        audioSource.Stop();
    }

    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += agent.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
}
