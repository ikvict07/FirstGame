using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image HPbar;

    public float fill;
    
    // Start is called before the first frame update
    void Start()
    {
        fill = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        HPbar.fillAmount = fill;
    }
}
