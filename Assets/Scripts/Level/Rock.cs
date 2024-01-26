using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rock : MonoBehaviour
{
    [SerializeField] private float _maxForce = 5000;
    [SerializeField] private float _minForce = 1;
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private ScoreManager _score => FindObjectOfType<ScoreManager>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Spit")) return;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        Vector2 targetDirection = transform.position - collision.transform.position;
        _rb.AddForce(targetDirection * Random.Range(_minForce, _maxForce));
        _score.IncrementChaos();
    }
}
