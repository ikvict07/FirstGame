using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public float Health;
    private AlienTakeDamage alienTakeDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        Health = 100f;
        alienTakeDamage = GetComponent<AlienTakeDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        alienTakeDamage.TakeDamage(damage);

    }
}
