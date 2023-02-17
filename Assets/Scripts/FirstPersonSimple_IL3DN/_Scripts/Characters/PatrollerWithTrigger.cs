using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollerWithTrigger : MonoBehaviour
{
    NavMeshAgent agent;

    private Animator anim;
    public Transform targetPlayer;

    bool patrolling;
    public Transform[] patrolTargets; //usar empty game objects
    private int destinationPoint;
    bool arrived;
    bool targetClose;


    private void Start()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetClose = true;
            patrolling = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetClose = false;
            patrolling = true;
        }
    }


    private void Update()
    {
       anim.SetFloat("Forward", agent.velocity.sqrMagnitude); //Animator speed

        if (agent.pathPending)
        {
            return; //if the agent is still trying to figure out where to go we just want to wait until it's done
        }
        if (patrolling)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (!arrived)
                {
                    arrived = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
            else
            {
                arrived = false;
            }
        }

        if (targetClose)
        {
            agent.SetDestination(targetPlayer.transform.position);
            patrolling = false;

        }
        else
        {
            if (!targetClose)
            {
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    patrolling = true; // if he got to the last know position, he stops following (because he would have see the target otherwise)
                    StartCoroutine("GoToNextPoint");
                }
            }
        }
    }

       IEnumerator GoToNextPoint()
    {
        if (patrolTargets.Length == 0)
        {
            yield break;
        }
        patrolling = true;
        yield return new WaitForSeconds(2f);
        arrived = false;
        agent.destination = patrolTargets[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % patrolTargets.Length; // % (modulo) gives you the remainder of those two numbers. To cycle destPoint back to zero if it get larger than patrolTargets.Lengh
    }        
}
