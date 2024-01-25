using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTongue : MonoBehaviour
{
    [SerializeField] private AnimationCurve _hoverCurve;
    [SerializeField] private Rigidbody2D _head;
    [SerializeField] ManageBodyWeight _body;
    private bool _grounded = false;
    private Coroutine _hoverCoroutine;

    internal void Spin()
    {
        //TODO: Play animation/ make a hinge joint motor spin
        Debug.Log("Play animation/ make a hinge joint motor spin");
    }
    internal void TryHover()
    {
        if (_grounded) return;
        //TODO: Add a force from the face up
        Debug.Log("Add a force from the face up");
        _hoverCoroutine = StartCoroutine(Hover());
    }

    private IEnumerator Hover()
    {
        _body.IsFlying = true;
        float timeElapse = 0;
        while(!_grounded)
        {
            yield return null;
            _head.AddForce(Vector2.up * _hoverCurve.Evaluate(timeElapse) * 1000);
            timeElapse += Time.deltaTime;
        }
    }

    internal void StopSpinning()
    {
        //TODO: Stop playing the animation/Turn off the motor
        Debug.Log("Stop playing the animation/Turn off the motor");
    }
    internal void Recharge()
    {
        //TODO: Start a timer to reset the cooldown
        Debug.Log("Start a timer to reset the cooldown");
        StopCoroutine(_hoverCoroutine);
        _body.IsFlying = false;
    }

}
