using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    public bool isPointerDown = false;
    private float pointerDownTimer = 0f;
    private float requiredHoldTime = 0.3f; // время, необходимое для срабатывания долгого зажатия
    private Animator animator;
    private KnightMovement movement;
    private bool heldDown;
    [Range(0, 1)] public float holdingTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<KnightMovement>();
    }
    

    public void OnPointerDown( )
    {
        isPointerDown = true;

        StartCoroutine(Timer());
    }

    public void OnPointerUp( )
    {
        isPointerDown = false;
        heldDown = false;
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
        if (heldDown)
        {
            // Действие при зажатии на кнопку
            Debug.Log("Button held down");
            heldDown = false;
        }
        else if (!heldDown)
        {
            Debug.Log("Single Click");
        }
    }

}