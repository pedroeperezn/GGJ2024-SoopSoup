using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBodyWeight : MonoBehaviour
{
    [SerializeField] private AnimationCurve _scaleModifier;
    internal int CurrentlyMovingLimbCount = 0;
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

    public void ManageGravityScale()
    {
        Debug.Log($"{CurrentlyMovingLimbCount} = {_scaleModifier.Evaluate(CurrentlyMovingLimbCount)}");
        _rb.AddForce(Vector2.down * _scaleModifier.Evaluate(CurrentlyMovingLimbCount));
    }

    public void ManageGravityScale2()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
        if (hit.collider == null)
        {
            _rb.gravityScale = 1;
            return;
        }

        float distance = Mathf.Abs(hit.point.y - transform.position.y);
        Debug.Log(distance);
        _rb.gravityScale = _scaleModifier.Evaluate(distance);
    }

    private void Update()
    {
        ManageGravityScale2();
    }
}
