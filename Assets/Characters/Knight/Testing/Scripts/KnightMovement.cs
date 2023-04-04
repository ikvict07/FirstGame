/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : Subject
{
    
    private Animator _anim;
    private Rigidbody2D _rb;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    
    private bool _isGrounded = false;
    private const float GroundRadius = 0.2f;

    public float maxSpeed = 10f;
    
    public bool isFacingRight = true;

    private float _move;
    
    private AnimFunctionsKnight _animfuncs;

    private const string AnimatorIsGroundedName = "isGrounded";
    private const string AnimatorSpeedName = "Speed";
    private const string AnimatorFacingRight = "FacingRight";

    

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _animfuncs = GetComponent<AnimFunctionsKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //RunningAnimation
    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundRadius, whatIsGround);
        _anim.SetBool("isGrounded", _isGrounded);
        bool isAttacking = _anim.GetBool("Attacking");
        
        if (!_isGrounded || isAttacking)
            return;
        
        
        _move = Input.GetAxis("Horizontal");
        
        NotifyObservers(PlayerActions.Run);
        
        _anim.SetFloat("Speed", Mathf.Abs(_move));
        _rb.velocity = new Vector2(_move * maxSpeed, _rb.velocity.y);
        
        if (_move > 0 && !isFacingRight)

            Flip();

        else if (_move < 0 && isFacingRight)
            Flip();
        
        
    }
    
    //Switch Side 
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        
        _anim.SetBool("FacingRight",isFacingRight);

        theScale.x *= -1;
        transform.localScale = theScale;
        
        if (!_animfuncs.idleForWhile)
        {
            if (isFacingRight)
            {
                _anim.Play("RunRight");
                _animfuncs.idleForWhile = false;
            }
            else
            {
                _anim.Play("RunLeft");
                _animfuncs.idleForWhile = false;
            }

        }
        else
        {
            if (isFacingRight)
            {
                _anim.Play("IdleRight");
            }
            else
            {
                _anim.Play("IdleLeft");
            }
        }
    }
}
*/
using System.Collections;
using UnityEngine;

public class KnightMovement : Subject
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private bool isGrounded;
    private const float GroundRadius = 0.2f;
    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public bool isFacingRight = true;
    [SerializeField] public float move;
    [SerializeField] private AnimFunctionsKnight animfuncs;
    private const string AnimatorIsGroundedName = "isGrounded";
    private const string AnimatorSpeedName = "Speed";
    private const string AnimatorFacingRight = "FacingRight";

    [SerializeField] public Transform knightTransform;

    private const string PlayerXPositionKey = "PlayerXPosition";
    private const string PlayerYPositionKey = "PlayerYPosition";

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animfuncs = GetComponent<AnimFunctionsKnight>();

        // Load the player position from PlayerPrefs
        float x = PlayerPrefs.GetFloat(PlayerXPositionKey);
        float y = PlayerPrefs.GetFloat(PlayerYPositionKey);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundRadius, whatIsGround);
        anim.SetBool(AnimatorIsGroundedName, isGrounded);
        
        if (!isGrounded || anim.GetBool("Attacking"))
            return;
        
        move = Input.GetAxis("Horizontal");
        NotifyObservers(PlayerActions.Run);
        anim.SetFloat(AnimatorSpeedName, Mathf.Abs(move));
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
        
        if (move > 0 && !isFacingRight || move < 0 && isFacingRight)
            Flip();
    }
    
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        anim.SetBool(AnimatorFacingRight, isFacingRight);
        theScale.x *= -1;
        transform.localScale = theScale;
        string animationName = isFacingRight ? "RunRight" : "RunLeft";
        
        if (!animfuncs.idleForWhile)
        {
            anim.Play(animationName);
            animfuncs.idleForWhile = false;
        }
        else
        {
            animationName = isFacingRight ? "IdleRight" : "IdleLeft";
            anim.Play(animationName);
        }
    }

    void OnApplicationQuit()
    {
        // Save the player position to PlayerPrefs
        PlayerPrefs.SetFloat(PlayerXPositionKey, transform.position.x);
        PlayerPrefs.SetFloat(PlayerYPositionKey, transform.position.y);
        PlayerPrefs.Save();
    }
}

