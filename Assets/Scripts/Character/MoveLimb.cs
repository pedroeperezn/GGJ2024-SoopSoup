using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveLimb : MonoBehaviour
{
    private Rigidbody2D _rb;
    internal float Strength = 1000000;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
 
    public void MoveInDirection(Vector2 targetInWorldSpace)
    {
        Vector2 targetDirection = (targetInWorldSpace - (Vector2)transform.position).normalized;

        Debug.Log(targetDirection);
        _rb.AddForce(targetDirection * Strength * Time.deltaTime);
    }
}
