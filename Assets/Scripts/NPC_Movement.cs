using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;



    private Transform wayPoint;
    private int wayPointNumber = 0;
    private int count;

    [SerializeField] public List<Transform> points = new List<Transform>();

    [SerializeField] private float[] secondsWaiting;


    private bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        wayPoint = points[wayPointNumber];
        if (canMove)
        {
            agent.SetDestination(wayPoint.position);
            hasArrived();
        }
        idleToWalking();
    }

    IEnumerator Waiting()
    {

        yield return new WaitForSeconds(secondsWaiting[wayPointNumber]);


        count = points.Count - 1;
        if (count == wayPointNumber)
        {
            wayPointNumber = 0;
        }
        else
        {
            wayPointNumber++;
        }

        canMove = true;
    }
    private void idleToWalking()
    {
        animator.SetFloat("speed", agent.velocity.magnitude);
    }

    private void hasArrived()
    {

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                canMove = false;
                StartCoroutine(Waiting());

            }
        }
    }
}