using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAttackAnim : StateMachineBehaviour
{
    private EnemyAttack enemyAttack;
    
    private EnemyWalk enemyWalk;
    private GameObject knight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyWalk = animator.GetComponent<EnemyWalk>();
        enemyAttack = animator.GetComponent<EnemyAttack>();
        knight = GameObject.FindGameObjectWithTag("Knight");
        enemyAttack.didAttack = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyAttack.attackFrame && !enemyAttack.didAttack)
        {
            enemyAttack.DoAttack();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((knight.transform.position.x < animator.transform.position.x) && enemyWalk.isFacingRight)
        {
            enemyWalk.Flip();
        }
        else if ((knight.transform.position.x > animator.transform.position.x) && !enemyWalk.isFacingRight)
        {
            enemyWalk.Flip();
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
