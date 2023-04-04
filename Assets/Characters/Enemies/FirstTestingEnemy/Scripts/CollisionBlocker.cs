using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBlocker : MonoBehaviour
{
    public Transform targetToFollow;

    public EnemyScript enemyScript;

    public Collider2D thisCollider;
    public Collider2D toFollowCollider;
    
    [Range(0,1f)]
    public float offSet;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = targetToFollow.GetComponent<EnemyScript>();
        thisCollider = GetComponent<Collider2D>();
        toFollowCollider = targetToFollow.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(targetToFollow.position.x, targetToFollow.position.y, transform.position.z);
        transform.position = newPosition;
        
        thisCollider.offset = toFollowCollider.offset + new Vector2(offSet, 0);

    }
}
