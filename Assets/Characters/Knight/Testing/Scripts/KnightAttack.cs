/*
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    private KnightMovement knightmovement;
    private Animator anim;
    public Transform attackRangeTransform;
    private Rigidbody2D rb;
    public float attackRadius = 2.5f;
    public float attackCooldown = 0.7f;
    public int baseDamage = 40;

    private bool isFacingRight;

    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        knightmovement = GetComponent<KnightMovement>();
        isFacingRight = knightmovement.isFacingRight;
        attackRangeTransform = GameObject.FindWithTag("AttackRange").transform;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!canAttack) return;
            isFacingRight = knightmovement.isFacingRight;

            anim.SetBool("Attacking", true);
            rb.velocity = new Vector2(0, rb.velocity.y);
            
            switch (isFacingRight)
            {
                case true:
                    anim.Play("AttackRight");
                    break;
                    
                case false:
                    anim.Play("AttackLeft");
                    break;
            }
            
            StartCoroutine(AttackCD());

        }
    }

    IEnumerator AttackCD()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    void OnDrawGizmosSelected()
    {
        if (attackRangeTransform == null)
            return;
        
        Gizmos.DrawWireSphere(attackRangeTransform.position, attackRadius);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    [SerializeField] private KnightMovement knightMovement;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] public float attackRadius;
    [SerializeField] public float attackCooldown = 0.7f;
    [SerializeField] public int baseDamage = 40;
    [SerializeField] public bool doAttack;

    private bool canAttack = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        knightMovement = GetComponent<KnightMovement>();
        attackRangeTransform = GameObject.FindGameObjectWithTag("AttackRange").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        if (doAttack)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (!canAttack || anim.GetBool("Attacking")) yield break;
        doAttack = false;

        canAttack = false;
        // rb.velocity = new Vector2(0, rb.velocity.y);
        anim.Play(knightMovement.isFacingRight ? "AttackRight" : "AttackLeft");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackRangeTransform == null) return;
        Gizmos.DrawWireSphere(attackRangeTransform.position, attackRadius);
    }
}


