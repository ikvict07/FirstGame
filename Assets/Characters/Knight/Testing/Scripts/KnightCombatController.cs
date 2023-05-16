using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCombatController : KnightStats
{
    [SerializeField] public bool isInvincible;
    

    public KnightTakeDamage takeDamage;
    
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] public bool didAttack;

    [SerializeField] public bool attackFrame;
    
    private GameObject knight;

    [SerializeField] public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        attackRangeTransform = GameObject.FindGameObjectWithTag ("AttackRange")?.transform;
        enemyLayers = LayerMask.GetMask ("Enemy");
        knight = GameObject.FindGameObjectWithTag ("Knight");
        takeDamage = GetComponent<KnightTakeDamage>(); // Get an instance of TakeDamage from the game object
        attackRadius = GetComponent<KnightAttack>().attackRadius;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage._KnightTakeDamage(10f); // Call the non-static method on the instance
        }
    }

    public void DoAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll (attackRangeTransform?.position ?? Vector3.zero, attackRadius, enemyLayers);
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<EnemyScript>().TakeDamage(baseDamage); // From KnightStats
            didAttack = true;
        }
    }
}
