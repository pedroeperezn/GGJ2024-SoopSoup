using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spit : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _spitPool;
    [SerializeField] private float _knockBackForce = 100f;
    [SerializeField] private float _shootForce = 1000f;

    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    internal void TryShoot()
    {
        //Get a spit ball from the pool
        SpitBall currentSpitBall = GetSpitBall();
        if (currentSpitBall == null) return;

        // fire it toward the mouse
        Vector2 mousePosition = MouseHelper.GetMouseWorldSpace(_mainCamera);
        Vector2 target = (mousePosition - (Vector2)transform.position).normalized;

        currentSpitBall.Reposition(transform.position);
        Shoot(currentSpitBall, target);
    }

    private void Shoot(SpitBall spit, Vector2 direction)
    {
        _rb.AddForce(-direction * _knockBackForce, ForceMode2D.Impulse);
        spit.Fire(direction * _shootForce);
    }

    private SpitBall GetSpitBall()
    {
        for(int i = 0; i < _spitPool.childCount; i++)
        {
            if (_spitPool.GetChild(i).TryGetComponent(out SpitBall newSpitBall))
            {
                if (!newSpitBall.gameObject.activeInHierarchy)
                {
                    return newSpitBall;
                }
            }
        }
        return null;
    }
}
