using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SecondAttack : StateMachineBehaviour
{

    public KnightAttack knightAttack;
    private KnightCombatController knightCombatController;
    private int numOfAttack = 2;
    private AudioSource audioSource;
    [SerializeField]private AudioClip secondAttackSound;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ContinueAttack", false);
        knightAttack = animator.GetComponent<KnightAttack>();
        knightCombatController = animator.GetComponent<KnightCombatController>();
        knightCombatController.didAttack = false;
        
        audioSource = animator.GetComponent<AudioSource>();
        audioSource.clip = secondAttackSound;
        audioSource.Play();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (knightAttack.doAttack)
        {
            animator.SetBool("ContinueAttack", true);
            knightAttack.doAttack = false;

        }

        if (knightCombatController.attackFrame && !knightCombatController.didAttack)
        {
            knightCombatController.DoAttack(numOfAttack);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("ContinueAttack"))
        {
            animator.SetBool("Attacking", false);
        }

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

}
