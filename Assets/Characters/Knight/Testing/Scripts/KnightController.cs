using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    private Animator _animator;
    private KnightRecoil _recoil;

    private void Start()
    {
        _recoil = GetComponent<KnightRecoil>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_animator.GetBool("Attacking"))
        {
            _recoil.DoRecoil();
        }
    }
}
