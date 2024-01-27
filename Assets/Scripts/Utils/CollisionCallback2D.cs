using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCallback2D : MonoBehaviour
{
    ICollisionCallback _listener = null;
    public void Initialize<T>(T listener) where T : ICollisionCallback
    {
        _listener = listener;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _listener?.CollisionHandler(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _listener?.TriggerHandler(collision);
    }
}
