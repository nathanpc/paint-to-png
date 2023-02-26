using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animator_val : MonoBehaviour
{
    [SerializeField] private bool talking_one;
    [SerializeField] private bool talking_two;
    [SerializeField] private bool sitting;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        

        animator.SetBool("talking_one", talking_one);
        animator.SetBool("talking_two", talking_two);
        animator.SetBool("sitting", sitting);


    }

    // Update is called once per frame
    void Update()
    {
       
    }
   
   

}