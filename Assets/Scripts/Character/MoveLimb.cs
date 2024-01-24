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
    private ManageBodyWeight _body;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
        FindBody(GetComponent<HingeJoint2D>()).TryGetComponent(out _body);
        if (_body == null) Debug.LogError($"Body not found {gameObject.name}");
    }

    private GameObject FindBody(HingeJoint2D hinge)
    {
        Rigidbody2D connectedBody = hinge.connectedBody;
        Debug.Log(connectedBody == null || !connectedBody.TryGetComponent(out HingeJoint2D newHinge1));
        if (connectedBody == null || !connectedBody.TryGetComponent(out HingeJoint2D newHinge)) return connectedBody.gameObject;
        return FindBody(newHinge);
    }

    public void FreeLimb()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _spring.enabled = true;
        ++_body.CurrentlyMovingLimbCount;
        _body.ManageGravityScale();
    }
 
    public void MoveInDirection(Vector2 targetInWorldSpace)
    {
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
        --_body.CurrentlyMovingLimbCount;
    }
}
