using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimFunctionsKnight : MonoBehaviour
{
    public Animator anim;
    private bool Rolling;
    private float nextTimeRoll;
    private float rollCooldown = 0.15f;
    private Rigidbody2D rb;


    public bool idleForWhile = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void idleForWhileTrue()
    {
        idleForWhile = true;
    }

    public void idleForWhileFalse()
    {
        idleForWhile = false;
    }

    public void moveForvard(float length)
    {
        // length = 0.825
        //rb.velocity = new Vector2(3.5f, 0);
        Transform rb_transform = rb.transform;
        rb.transform.position = new Vector3(rb_transform.position.x + length, rb_transform.position.y , rb_transform.position.z);
    }

    public void stopAttacking()
    {
        if (!anim.GetBool("ContinueAttack"))
        {
            anim.SetBool("Attacking", false);
        }
    }

   
}
