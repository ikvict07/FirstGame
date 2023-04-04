
// /*
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class AnimKnightAttack : StateMachineBehaviour
// {
//
//     public Transform attackRangeTransform;
//     public float attackRadius;
//     public LayerMask enemyLayers;
//     public int baseDamage;
//     public KnightAttack knightAttack;
//     public GameObject knight;
//
//     
//     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         attackRangeTransform = GameObject.FindGameObjectWithTag("AttackRange").transform;
//         enemyLayers = LayerMask.GetMask("Enemy");
//         
//         knight = GameObject.FindGameObjectWithTag("Knight");
//         knightAttack = knight.GetComponent<KnightAttack>();
//         attackRadius = knightAttack.attackRadius;
//         baseDamage = knightAttack.baseDamage;
//         
//     }
//
//     // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//     override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         Attack_();
//     }
//     void Attack_()
//     {
//         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackRangeTransform.position, attackRadius, enemyLayers);
//         foreach (Collider2D enemy in hitEnemies)
//         {
//             //enemy.GetComponent<Enemy>().takeDamage(baseDamage);
//
//         }
//         
//     }
//
//     // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
//     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         animator.SetBool("Attacking", false);
//     }
//
//     // OnStateMove is called right after Animator.OnAnimatorMove()
//     //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     //{
//     //    // Implement code that processes and affects root motion
//     //}
//
//     // OnStateIK is called right after Animator.OnAnimatorIK()
//     //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     //{
//     //    // Implement code that sets up animation IK (inverse kinematics)
//     //}
// }
// */
using UnityEngine;

public class AnimKnightAttack : StateMachineBehaviour {

    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int baseDamage;

    private GameObject knight;

    public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        attackRangeTransform = GameObject.FindGameObjectWithTag ("AttackRange")?.transform;
        enemyLayers = LayerMask.GetMask ("Enemy");
        knight = GameObject.FindGameObjectWithTag ("Knight");
        animator.SetBool("Attacking", true);
    }

    public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("ContinueAttack", true);
        }
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll (attackRangeTransform?.position ?? Vector3.zero, attackRadius, enemyLayers);
        foreach (Collider2D enemy in hitEnemies) {
            //enemy.GetComponent<Enemy>().takeDamage(baseDamage);
        }
    }

    public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!animator.GetBool("ContinueAttack"))
        {
            animator.SetBool("Attacking", false);
        }
    }
}
