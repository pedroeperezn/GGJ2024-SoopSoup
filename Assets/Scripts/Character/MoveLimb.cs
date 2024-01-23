using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]
public class MoveLimb : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpringJoint2D _spring;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
    }
 
    public void MoveInDirection(Vector2 targetInWorldSpace, float strength)
    {
        _spring.enabled = true;
        _spring.connectedAnchor = targetInWorldSpace;

    }

    public void Stick()
    {
        _spring.enabled = false;
        //TODO: stick to a surface, probally check an overlap sphere and then freeze constraints or maybe something else
    }
}
