using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] public Transform followingTarget;

    [SerializeField, Range(0f, 1f)] public float parallaxStrength = 0.1f;
    [SerializeField] public bool disableVerticalParallax = false;
    private Vector3 targetPreviosusPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;
        targetPreviosusPosition = followingTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = followingTarget.position - targetPreviosusPosition;

        if (disableVerticalParallax)
            delta.y = 0;
        delta.z = 0;
        targetPreviosusPosition = followingTarget.position;
        transform.position += delta * parallaxStrength; 
    }
}
