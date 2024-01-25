using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpitBall : MonoBehaviour
{
    [SerializeField] private float _coolDownTime;
    private bool _collided = false;

    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    public void Fire(Vector2 force)
    {
        //AUDIO FIRE FOR SPIT GOES HERE
        gameObject.SetActive(true);
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void Reposition(Vector2 newPosition)
    {
        _collided = false;
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0;
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collided || !gameObject.activeInHierarchy) return;
        //ADUIO FIRE FOR SPIT COLLISION GOES HERE
        StartCoroutine(SlowDisappear());
    }

    private void OnBecameInvisible()
    {
        if (_collided || !gameObject.activeInHierarchy) return;
        StartCoroutine(SlowDisappear());
    }

    IEnumerator SlowDisappear()
    {
        _collided = true;
        yield return new WaitForSeconds(_coolDownTime);
        gameObject.SetActive(false);
    }
}
