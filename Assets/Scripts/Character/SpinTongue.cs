using FMOD.Studio;
using FMODUnity;
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

    [SerializeField] private EventInstance _helicopterEventInstance;

    // The UI will probably need this guy, he starts at zero and goes up to whatever _coolDownTime is
    internal float CoolDownTime = 0;

    private bool _cooledDown = true;
    private Coroutine _hoverCoroutine;

<<<<<<< HEAD

    private void Start()
    {
        _helicopterEventInstance = AudioManager.Instance.CreateInstance(FMODEventsManager.Instance.LlamaHelicopter);
    }

    internal void TryHover()
=======
    internal void TryHover(List<MoveLimb> limbs)
>>>>>>> d22db09 (Limbs release when helicoptering now)
    {
        if (!_cooledDown || _body.IsFlying) return;

        // release all a limbs
        foreach (MoveLimb limb in limbs)
        {
            limb.ReleaseLimb();
        }

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
        // AUDIO FOR HELICOPER GEOS HERE IF IT"S A LOOPING AUDIO PIECE
        _helicopterEventInstance.setParameterByName("HellicopterOff", 0);
        _helicopterEventInstance.start();
        _body.IsFlying = true;
        _cooledDown = false;
        float timeElapse = 0;
        while(timeElapse < _maxSpinTime)
        {
            yield return null;
            // OR THE AUDIO CAN GO HERE FOR THE HELICOPTER IF IT'S A ONE FIRE
            _head.AddForce(Vector2.up * _hoverCurve.Evaluate(timeElapse) * 200000 * Time.deltaTime);
            timeElapse += Time.deltaTime;
        }
        StopSpinning();
    }

    internal void StopSpinning()
    {
        _helicopterEventInstance.setParameterByName("HellicopterOff", 1);
        //_helicopterEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
