using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


public class PlayerMovement : MonoBehaviour
{
    private bool isGrounded = false;
    public Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    
    public float maxSpeed = 10f;
    public bool isFacingRight = true;

    
    private Animator anim;
    private Rigidbody2D rb;

    private bool Rolling;
    public float nextTimeRoll;
    private float rollCooldown = 0.15f;

    private float move;

    public bool idleForWhile;

    private AnimFunctions animfuncs;
    // Start is called before the first frame update
    void Start()
    {   
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        nextTimeRoll = Time.time;
        animfuncs = GetComponent<AnimFunctions>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded && (Time.time > nextTimeRoll))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Roll();
                nextTimeRoll = Time.time + 50f;
            }

        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("isGrounded", isGrounded);
        

        if (!isGrounded)
            return;

        move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rb.velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0 && !isFacingRight)

            Flip();

        else if (move < 0 && isFacingRight)
            Flip();
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;
        
        if (!animfuncs.idleForWhile)
        {
            anim.Play("Run");
            animfuncs.idleForWhile = false;
        }
        else
        {
            anim.Play("StartRun");
        }
    }


    private void Roll()
    {

        
        anim.Play("Roll");
        
        int rollingLayer = LayerMask.NameToLayer("RollingPlayer");
        gameObject.layer = rollingLayer;
        
        //rb.AddForce(new Vector2(3000 * (2 * Convert.ToInt32(isFacingRight) - 1), 100));
        
    }
    public void RollingDash()
    {   
        
        rb.AddForce(new Vector2(1000 * (2 * Convert.ToInt32(isFacingRight) - 1), 0));
        
    }
}
