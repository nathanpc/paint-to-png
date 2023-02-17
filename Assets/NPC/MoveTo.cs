using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    

    private Transform poiDestiny;
    private int poiNumber = 0;
    private int number;

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
        poiDestiny = points[poiNumber];
        if (canMove)
        {
            moveCube(poiDestiny);
            hasArrived();
        }
        idleToWalking();
    }
    private void moveCube(Transform poiTransform)
    {
        agent.SetDestination(poiTransform.position);
        // cube.jump;
    }


    IEnumerator Waiting()
    {

        yield return new WaitForSeconds(secondsWaiting[poiNumber]);
        

        number = points.Count - 1;
        if (number == poiNumber)
        {
            poiNumber = 0;
        }
        else
        {
            poiNumber++;
        }

        canMove = true;
    }
    private void idleToWalking()
    {
        //Debug.Log(agent.velocity.magnitude);
        animator.SetFloat("speed", agent.velocity.magnitude);

        

        //if (agent.remainingDistance <= 2)
        //{
        //    agent.speed = agent.remainingDistance;
        //}
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