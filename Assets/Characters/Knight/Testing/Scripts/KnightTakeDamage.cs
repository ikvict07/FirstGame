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
    
    private KnightCombatController controller;
    private bool invincible;
    private KnightStats stats;
    


    [SerializeField ]public void _KnightTakeDamage(float damage)
    {
        invincible = controller.isInvincible;
        if (!invincible)
        {
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




    // Update is called once per frame

}
