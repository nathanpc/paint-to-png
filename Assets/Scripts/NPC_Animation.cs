using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    private Animator controller;

    enum AnimationType { Sitting, LookingDown, TalkinginPhone, Walking_TalkingonPhone, Arguing , Yelling ,talking1, talking2}
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
        else if (animation_type == AnimationType.Walking_TalkingonPhone)
        {
            controller.SetTrigger("Walking&TalkingonPhone");
        }
        else if (animation_type == AnimationType.Arguing)
        {
            controller.SetTrigger("Arguing");
        }
        else if (animation_type == AnimationType.Yelling)
        {
            controller.SetTrigger("Yelling");
        }
        else if (animation_type == AnimationType.talking1)
        {
            controller.SetTrigger("talking1");
        }
        else if (animation_type == AnimationType.talking2)
        {
            controller.SetTrigger("talking2");
        }
    }
}
