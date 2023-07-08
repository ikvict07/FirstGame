using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    private KnightAttack attackScript;
    // Start is called before the first frame update
    void Start()
    {
        attackScript = GetComponent<KnightAttack>();
    }

    // Update is called once per frame
    public void DoAttack()
    {
        attackScript.doAttack = true;
    }
}
