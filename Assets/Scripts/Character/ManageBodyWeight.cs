using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBodyWeight : MonoBehaviour
{
    [SerializeField] private float _limbMass = 15f;
    [SerializeField] private AnimationCurve _scaleModifier;
    internal int CurrentlyMovingLimbCount = 0;
    internal bool[] IsSticking = new bool[5];
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

    public void ManageGravityScale()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
        if (hit.collider == null || Array.IndexOf(IsSticking, true) != -1)
        {
            _rb.gravityScale = 1;
            return;
        }

        float distance = Mathf.Abs(hit.point.y - transform.position.y);
        _rb.gravityScale = _scaleModifier.Evaluate(distance) * ((CurrentlyMovingLimbCount > 1) ? CurrentlyMovingLimbCount : 1);
    }

    public void ManageLimbWeight(Rigidbody2D limb)
    {
        if (CurrentlyMovingLimbCount > 1)
        {
            limb.mass = 1;
        }
        else
        {
            limb.mass = _limbMass;
        }
    }

    private void Update()
    {
        ManageGravityScale();
    }
}
