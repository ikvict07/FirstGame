using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public Transform playerTransform;
    public Animator animator;
    public float walkSpeed = 2f;
    public bool isFacingRight = true;
    public Rigidbody2D rb;
    public float seeDistance = 10f;
    public float stopDistance = 1.5f;
    
    private bool stopWalking = false;
    private bool isTooClose;
    
    private AlienTakeDamage alienTakeDamageScript;

    
    // Start is called before the first frame update
    void Start()
    {
        alienTakeDamageScript = GetComponent<AlienTakeDamage>();

        playerTransform = GameObject.FindGameObjectWithTag("Knight").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("isAttacking"))
        {
            if (playerTransform.position.x < transform.position.x && isFacingRight) Flip();
            if (playerTransform.position.x > transform.position.x && !isFacingRight) Flip();
            Walk();
        }


        if (isTooClose)
        {
            StartCoroutine(DontWalkForAWhile());
        }
    }

    private bool SeePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        bool isWithinSeeRange = distanceToPlayer < seeDistance;
        isTooClose = distanceToPlayer < stopDistance;

        bool result = isWithinSeeRange && !isTooClose && !animator.GetBool("isAttacking");
        return result;
    }

    void Walk()
    {
        if (alienTakeDamageScript.stunned || stopWalking || animator.GetBool("isAttacking")) return;

        
        bool isWalking = SeePlayer();
        animator.SetBool("isWalking", isWalking);

        if (!isWalking)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        transform.position = Vector2.MoveTowards(
            transform.position,
            playerTransform.position,
            walkSpeed * Time.deltaTime);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private IEnumerator DontWalkForAWhile()
    {
        stopWalking = true;
        yield return new WaitForSeconds(2);
        stopWalking = false;
    }
}
