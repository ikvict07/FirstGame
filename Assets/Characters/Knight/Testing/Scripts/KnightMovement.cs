
using System.Collections;
using Unity.VisualScripting;
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

        anim.SetFloat(AnimatorSpeedName, Mathf.Abs(move));
        if (move != 0) // Когда бежит
        {
            anim.updateMode = AnimatorUpdateMode.Normal;
            //move = Input.GetAxis("Horizontal");
            NotifyObservers(PlayerActions.Run); //Notify observers
            rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

            if (move > 0 && !isFacingRight || move < 0 && isFacingRight)
                Flip();
        }
        else
        {
            anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
        }
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

    public void onButtonPress(float direction)
    {
        move = direction;
    }

    void OnApplicationQuit()
    {
        // Save the player position to PlayerPrefs
        PlayerPrefs.SetFloat(PlayerXPositionKey, transform.position.x);
        PlayerPrefs.SetFloat(PlayerYPositionKey, transform.position.y);
        PlayerPrefs.Save();
    }
    
}

