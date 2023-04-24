using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.Animations;

public class NPC_Animation : MonoBehaviour
{
    private Animator controller;

    enum AnimationType { Sitting, LookingDown, TalkinginPhone}
    [SerializeField] private AnimationType animation_type;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();

        if (animation_type == AnimationType.Sitting)
        {
            controller.SetTrigger("Sitting");
        }
        else if (animation_type == AnimationType.LookingDown)
        {
            controller.SetTrigger("Looking_Down");
        }
        else if (animation_type == AnimationType.TalkinginPhone)
        {
            controller.SetTrigger("TalkingonPhone");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
