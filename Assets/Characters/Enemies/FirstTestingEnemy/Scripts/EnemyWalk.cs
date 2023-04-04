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
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Knight").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x < transform.position.x && isFacingRight) Flip();
        if (playerTransform.position.x > transform.position.x && !isFacingRight) Flip();
        Walk();
    }

    private bool SeePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        bool isWithinSeeRange = distanceToPlayer < seeDistance;
        bool isTooClose = distanceToPlayer < stopDistance;

        bool result = isWithinSeeRange && !isTooClose;
        return result;
    }

    void Walk()
    {
        bool isWalking = SeePlayer();
        animator.SetBool("isWalking", isWalking);

        if (!isWalking)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = new Vector2((isFacingRight ? 1 : -1) * walkSpeed, rb.velocity.y);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
