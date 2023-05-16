using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDash : MonoBehaviour
{
    private KnightCombatController controller;
    private Animator animator;
    private KnightMovement movement;
    private bool isFacingRight;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<KnightCombatController>();
        animator = GetComponent<Animator>();
        movement = GetComponent<KnightMovement>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void DoDash()
    {
        isFacingRight = movement.isFacingRight;

        StartCoroutine(BecomeInvincible());
        animator.Play(isFacingRight ? "DashRight" : "DashLeft");
    }

    private IEnumerator BecomeInvincible() //invincible 
    {
        controller.isInvincible = true;
        yield return new WaitForSeconds(15 / 24f); //15 frames 24 fps
        controller.isInvincible = false;

    }
}

