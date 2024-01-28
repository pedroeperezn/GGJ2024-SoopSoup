using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpinTongue : MonoBehaviour
{
    [SerializeField] private AnimationCurve _hoverCurve;
    [SerializeField] private Rigidbody2D _head;
    [SerializeField] ManageBodyWeight _body;
    [SerializeField] Transform _jointToSpin;
    [SerializeField] private float _maxSpinTime = 10;
    [SerializeField] private float _coolDownTime = 3;

    [HideInInspector] public UnityEvent StartRecharge = new UnityEvent();
    [HideInInspector] public UnityEvent StartHover = new UnityEvent();

    // The UI will probably need this guy, he starts at zero and goes up to whatever _coolDownTime is
    internal float CurrentTime = 0;
    internal float CoolDownTime => _coolDownTime;
    internal float MaxSpinTime => _maxSpinTime;

    private bool _cooledDown = true;
    private Coroutine _hoverCoroutine;

    private ScoreManager _score => FindObjectOfType<ScoreManager>();

    //Audio
    private EventInstance _helicopterEventInstance;
    private bool _haveTouristsCheered;

    private void Start()
    {
        _helicopterEventInstance = AudioManager.Instance.CreateInstance(FMODEventsManager.Instance.LlamaHelicopter);
    }


    internal void TryHover(List<MoveLimb> limbs)
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

        StartHover.Invoke();
        _hoverCoroutine = StartCoroutine(Hover());
    }

    private IEnumerator Hover()
    {
        // AUDIO FOR HELICOPER GEOS HERE IF IT"S A LOOPING AUDIO PIECE
        _helicopterEventInstance.setParameterByName("HellicopterOff", 0);
        _helicopterEventInstance.start();
        _body.IsFlying = true;
        _cooledDown = false;
        CurrentTime = 0;
        while(CurrentTime < _maxSpinTime)
        {
            yield return null;
            // OR THE AUDIO CAN GO HERE FOR THE HELICOPTER IF IT'S A ONE FIRE
            _head.AddForce(Vector2.up * _hoverCurve.Evaluate(CurrentTime) * 200000 * Time.deltaTime);
            CurrentTime += Time.deltaTime;
            if(!_haveTouristsCheered) 
            {
                for (int i = 0; i < _score.PeopleCount; i++)
                { 
                    AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.TouristAmazed, this.transform.position);
                }
                _haveTouristsCheered = true;
            }
        }
        StopSpinning();
        Recharge();
    }

    internal void StopSpinning()
    {
        _helicopterEventInstance.setParameterByName("HellicopterOff", 1);
        _haveTouristsCheered = false;
        //Stop playing the animation/Turn off the motor
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).TryGetComponent(out HingeJoint2D hinge);
            if (hinge) hinge.useMotor = false;
        }

        for (int i = 0; i < _score.PeopleCount; i++)
        {
            AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.TouristScared, this.transform.position);
        }

        StopCoroutine(_hoverCoroutine);
    }
    internal void Recharge()
    {
        if (!_body.IsFlying) return;
        //Start a timer to reset the cooldown
        _body.IsFlying = false;
        StartCoroutine(CoolDown());
        StartRecharge.Invoke();
    }

    private IEnumerator CoolDown()
    {
        // This guy might need to remove depending on how the UI works
        CurrentTime = 0;
        while (CurrentTime < _coolDownTime)
        {
            yield return null;
            CurrentTime += Time.deltaTime;
        }
        _cooledDown = true;
    }
}
