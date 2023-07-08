using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KnightTakeDamage : MonoBehaviour
{   
    public SpriteRenderer spriteRenderer;
    public Material redFlashMaterial;
    public Material defaultMaterial;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image SecondHpBar;
    
    private KnightCombatController controller;
    private bool invincible;
    private KnightStats stats;
    
    [SerializeField] private float durationOfInterpolation;
    [SerializeField] private float waitingTime;
    


    public void _KnightTakeDamage(float damage)
    {
        invincible = controller.isInvincible;
        if (!invincible)
        {
            SmoothHpChange(controller.currentHealth, damage);
            Debug.Log(controller.currentHealth + " " + damage);
            controller.currentHealth -= damage; 
            HPLoseAnimation();
            spriteRenderer.material = redFlashMaterial;
            StartCoroutine(BecomeNormal()); 
        }

    }

    public IEnumerator BecomeNormal()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<KnightCombatController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<KnightStats>();
    }

    public void HPLoseAnimation()
    {
        hpBar.fillAmount = controller.currentHealth / stats.MaxHealth;
    }
    
    public void SmoothHpChange(float currentHp, float damage )
    {
        StartCoroutine(SmoothInterpolation(currentHp, damage));
    }
    IEnumerator SmoothInterpolation(float currentHp, float damage)
    {
        float timer = 0f;
        float startingHp = currentHp;
        float targetHp = currentHp - damage;
        yield return new WaitForSeconds(waitingTime);
        while (timer < durationOfInterpolation)
        {
            float t = Mathf.Clamp01(timer / durationOfInterpolation);

            float interpolatedHp = Mathf.Lerp(startingHp, targetHp, t);

            SecondHpBar.fillAmount = interpolatedHp;

            timer += Time.deltaTime;
            SecondHpBar.fillAmount = interpolatedHp / controller.MaxHealth;
            yield return null;
        }
        
    }




    // Update is called once per frame

}
