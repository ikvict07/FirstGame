using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bushScript : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnBecameVisible()
    {
        anim.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
