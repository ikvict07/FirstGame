using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    private EnemyWalk enemyWalk;
    private Animator animator;
    private float stopDistance;
    public Transform playerTransform;
    private bool canAttack = true;
    private AlienTakeDamage alienTakeDamageScript;

    private Transform attackRangeTransform;

    [SerializeField] private float attackRadius = 3f;
  

    [Range (0,10)]
    [SerializeField]public float attackCooldown;

    [SerializeField] private int playerLayer;
    

    [SerializeField] public bool attackFrame; //From Script

    [SerializeField] private float baseDamage = 20f;

    public bool didAttack = false;
    
    [SerializeField]
    private bool attackFrameCheck;

    // Start is called before the first frame update
    void Start()
    {
        alienTakeDamageScript = GetComponent<AlienTakeDamage>();
        playerTransform = GameObject.FindGameObjectWithTag("Knight").transform;
        animator = GetComponent<Animator>();
        enemyWalk = GetComponent<EnemyWalk>();
        stopDistance = enemyWalk.stopDistance;
        attackRangeTransform = transform.GetChild(0);
        
        playerLayer = LayerMask.GetMask ("Player");
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Vector2.Distance(transform.position, playerTransform.position) <= stopDistance)
        {
            StartCoroutine(Attack());
        }
        
    }

    IEnumerator Attack()
    {
        if (alienTakeDamageScript.stunned) yield break;
        if (canAttack)
        {
            canAttack = false;
            animator.SetBool("isAttacking", true);
            animator.Play("AlienAttack");
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
    
    public void DoAttack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll (attackRangeTransform?.position ?? Vector3.zero, attackRadius, playerLayer);
        foreach (Collider2D player in hitPlayers) {
            player.GetComponent<KnightTakeDamage>()._KnightTakeDamage(baseDamage);
            didAttack = true;

        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackRangeTransform == null) return;
        Gizmos.DrawWireSphere(attackRangeTransform.position, attackRadius);
    }
    
}
