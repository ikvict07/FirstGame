using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunButton : MonoBehaviour
{
    public bool isPointerDown = false;
    private float pointerDownTimer = 0f;
    private float requiredHoldTime = 0.3f; // время, необходимое для срабатывания долгого зажатия

    private KnightMovement movement;
    private bool heldDown;
    [Range(0, 1)] public float holdingTime;

    private bool rightButton;
    private float move;
    
    private Animator animator;
    
    private KnightRecoil recoil;
    private KnightDash dash;

    private bool onCoolDown;


    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<KnightMovement>();
        recoil = GetComponent<KnightRecoil>();
        dash = GetComponent<KnightDash>();
    }
    

    public void OnPointerDown(bool rightButton)
    {
        if (onCoolDown) return;

        StartCoroutine(ButtonCoolDown());
        
        this.rightButton = rightButton;
        isPointerDown = true;
    
        StartCoroutine(Timer());
    }

    public void OnPointerUp(bool rightButton)
    {
        isPointerDown = false;
        heldDown = false;
        move = 0;
        movement.onButtonPress(move);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(holdingTime);
        if (isPointerDown)
        {
            heldDown = true;
        }
        else
        {
            heldDown = false;
        }
        OnCLick();
    }

    void OnCLick()
    {
        if (onCoolDown) return;
        if (heldDown)
        {
            // Действие при зажатии на кнопку
            
            
            move = rightButton ? 1 : -1;
            movement.onButtonPress(move);
            
            
            heldDown = false;
        }
        else if (!heldDown)
        {
            var isFacingRight = movement.isFacingRight;
            
            if (rightButton)
            {
                if (isFacingRight) dash.DoDash();
                else recoil.DoRecoil();
            }
            else
            {
                if (isFacingRight) recoil.DoRecoil();
                else dash.DoDash();
            }
        }
    }

    IEnumerator ButtonCoolDown()
    {
        onCoolDown = true;
        yield return new WaitForSeconds(holdingTime); // Чтобы не спамили кнопку
        onCoolDown = false;
    }
}
