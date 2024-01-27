using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticTourist : MonoBehaviour, ICollisionCallback
{
    [SerializeField] private Rigidbody2D[] _anchors;

    private void Awake()
    {
        for(int i = 0; i < transform.childCount; ++i) 
        {
            if (transform.GetChild(i).TryGetComponent(out Collider2D bodyPart))
            {
                CollisionCallback2D bodyCollision = bodyPart.AddComponent<CollisionCallback2D>();
                bodyCollision.Initialize(this);
            }
        }
    }
    public void CollisionHandler(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Llama") || collisionInfo.gameObject.layer == LayerMask.NameToLayer("Spit"))
        {
            //Ouch Audio would go here
            foreach(Rigidbody2D rb in _anchors)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    public void TriggerHandler(Collider2D other)
    {
        Debug.Log("Trigger");
    }
}
