using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public float MaxHealth;
    [FormerlySerializedAs("Health")] [SerializeField] public float CurrentHealth;
    [SerializeField]private TakeDamage takeDamage;
    [SerializeField] private Die dieScript;
    [SerializeField] public bool isDead = false;


    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage, int numOfAttack)
    {
        takeDamage._TakeDamage(damage, numOfAttack);
    }

    private void Update()
    {
        if (CurrentHealth <= 0 && !isDead)
        {
            isDead = true;
            dieScript._Die();
        }
    }
}
