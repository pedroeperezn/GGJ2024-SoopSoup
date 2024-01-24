using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]
public class MoveLimb : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpringJoint2D _spring;
    [SerializeField] private float _grabRadius = 1.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
    }
 
    public void MoveInDirection(Vector2 targetInWorldSpace, float strength)
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _spring.enabled = true;
        _spring.connectedAnchor = targetInWorldSpace;

    }

    public void TryToStick()
    {
        //disable the spring and check to see if we're near a stickable surface, if we are freeze the rb else just flop
        _spring.enabled = false;
        Collider2D[] objectNear = Physics2D.OverlapCircleAll(transform.position, _grabRadius, LayerMask.GetMask("Ground"));
        if (objectNear.Length > 0)
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
