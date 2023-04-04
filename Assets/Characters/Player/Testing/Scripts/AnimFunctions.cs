using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFunctions : MonoBehaviour
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
    public void idleForWhileCheck()
    {
        idleForWhile = true;
    }

    public void idleForWhileFalse()
    {
        idleForWhile = false;
    }





}
