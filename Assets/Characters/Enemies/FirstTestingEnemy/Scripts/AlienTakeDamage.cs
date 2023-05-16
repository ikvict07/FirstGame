using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTakeDamage : MonoBehaviour
{   
    public SpriteRenderer spriteRenderer;
    public Material redFlashMaterial;
    public Material defaultMaterial;
    private EnemyScript enemyScript;

    public float knockbackTime = 0.15f;
    public float knockbackDistance = 2f;


    public bool stunned;
    [Range(0,5)]
    public float stunDuration = 1f;

    private bool isFacingRight;

    [SerializeField] private EnemyWalk enemyWalk;

    [SerializeField ]public void TakeDamage(float damage)
    {
        spriteRenderer.material = redFlashMaterial;
        ApplyKnockback(isFacingRight);
        StartCoroutine(Stun());
        StartCoroutine(BecomeNormal());
        enemyScript.Health -= damage;
        
    }

    public IEnumerator BecomeNormal()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyWalk = GetComponent<EnemyWalk>();

    }

    public IEnumerator Stun()
    {
        stunned = true;
        yield return new WaitForSeconds(stunDuration);
        stunned = false;
    }
    

    void ApplyKnockback(bool knockbackDirection)
    {
        isFacingRight = enemyWalk.isFacingRight;
        Vector2 knockbackAmount = new Vector2(isFacingRight ? knockbackDistance : -knockbackDistance, 0);
        StartCoroutine(KnockbackCoroutine(new Vector3(knockbackAmount.x, knockbackAmount.y, 0f)));
    }

    IEnumerator KnockbackCoroutine(Vector3 knockbackAmount)
    {
        Vector3 startPosition = transform.position;
        float timer = 0f;

        while (timer < knockbackTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, startPosition - knockbackAmount, timer / knockbackTime);
            yield return null;
        }
    }

}