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

    // The UI will probably need this guy, he starts at zero and goes up to whatever _coolDownTime is
    internal float CoolDownTime = 0;

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
        // AUDIO FOR HELICOPER GEOS HERE IF IT"S A LOOPING AUDIO PICE
        _body.IsFlying = true;
        _cooledDown = false;
        float timeElapse = 0;
        while(timeElapse < _maxSpinTime)
        {
            yield return null;
            // OR THE AUDIO CAN GO HERE FOR THE HELICOPTER IF IT'S A ONE FIRE
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
        // This guy might need to remove depending on how the UI works
        CoolDownTime = 0;
        while (CoolDownTime < _coolDownTime)
        {
            yield return null;
            CoolDownTime += Time.deltaTime;
        }
        _cooledDown = true;
    }
}
