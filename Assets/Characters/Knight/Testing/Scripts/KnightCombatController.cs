using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class KnightCombatController : KnightStats
{
    [SerializeField] public bool isInvincible;
    

    public KnightTakeDamage takeDamage;
    
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] public bool didAttack;

    [SerializeField] public bool attackFrame;
    

    [SerializeField] public float currentHealth;
    
    [SerializeField] private Image hpBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        attackRangeTransform = GameObject.FindGameObjectWithTag ("AttackRange")?.transform;
        enemyLayers = LayerMask.GetMask ("Enemy");
        takeDamage = GetComponent<KnightTakeDamage>(); // Get an instance of TakeDamage from the game object
        attackRadius = GetComponent<KnightAttack>().attackRadius;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        hpBar.fillAmount = currentHealth / MaxHealth;
    }

    public void DoAttack(int numOfAttack)
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll (attackRangeTransform?.position ?? Vector3.zero, attackRadius, enemyLayers);


        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(baseDamage, numOfAttack);

            didAttack = true;
        }

    }
}
