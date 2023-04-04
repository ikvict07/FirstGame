using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private float nextTimeRoll;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        rb = animator.GetComponent<Rigidbody2D>();
        bool isFacingRight = animator.GetComponent<PlayerMovement>().isFacingRight;
        
        void DisableCollider()
        {
            //transform.Find("CharacterColussionBlocker").GetComponent<CapsuleCollider2D>().enabled = false;
        }
        rb.AddForce(new Vector2(3000 * (2 * Convert.ToInt32(isFacingRight) - 1), 100));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     
    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        void StartIdle()
        {
            //transform.Find("CharacterColussionBlocker").GetComponent<CapsuleCollider2D>().enabled = true;
            int playerLayer = LayerMask.NameToLayer("Player");
            animator.gameObject.layer = playerLayer;
            animator.Play("Idle");
            nextTimeRoll = Time.time + 0.5f;
            animator.GetComponent<PlayerMovement>().nextTimeRoll = nextTimeRoll;
            
        }
        StartIdle();
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
