using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTongue : MonoBehaviour
{
    [SerializeField] private AnimationCurve _hoverCurve;
    [SerializeField] private Rigidbody2D _head;
    [SerializeField] ManageBodyWeight _body;
    [SerializeField] Transform _jointToSpin;
    [SerializeField] private float _maxSpinTime = 10;
    [SerializeField] private float _coolDownTime = 3;

    private bool _cooledDown = true;
    private Coroutine _hoverCoroutine;

    internal void TryHover()
    {
        if (!_cooledDown || _body.IsFlying) return;
        //Add a force from the face up
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).TryGetComponent(out HingeJoint2D hinge);
            if (hinge) hinge.useMotor = true;
        }
        _hoverCoroutine = StartCoroutine(Hover());
    }

    private IEnumerator Hover()
    {
        _body.IsFlying = true;
        _cooledDown = false;
        float timeElapse = 0;
        while(timeElapse < _maxSpinTime)
        {
            yield return null;
            //_jointToSpin.Rotate(0, 0, _spinSpeed * Time.deltaTime);
            _head.AddForce(Vector2.up * _hoverCurve.Evaluate(timeElapse) * 1000);
            timeElapse += Time.deltaTime;
        }
        StopSpinning();
    }

    internal void StopSpinning()
    {
        //Stop playing the animation/Turn off the motor
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).TryGetComponent(out HingeJoint2D hinge);
            if (hinge) hinge.useMotor = false;
        }
        StopCoroutine(_hoverCoroutine);
    }
    internal void Recharge()
    {
        //Start a timer to reset the cooldown
        _body.IsFlying = false;
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_coolDownTime);
        _cooledDown = true;
    }
}
