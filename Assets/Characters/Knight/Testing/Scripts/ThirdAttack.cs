using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdAttack : StateMachineBehaviour
{
    private KnightCombatController knightCombatController;

    private int numOfAttack = 3;
    private AudioSource audioSource;
    [SerializeField]private AudioClip thirdAttackSound;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knightCombatController = animator.GetComponent<KnightCombatController>();
        knightCombatController.didAttack = false;
        
        audioSource = animator.GetComponent<AudioSource>();
        audioSource.clip = thirdAttackSound;
        audioSource.Play();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (knightCombatController.attackFrame && !knightCombatController.didAttack)
        {
            knightCombatController.DoAttack(numOfAttack);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("ContinueAttack", false);
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
