using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField] private List<MoveLimb> _limbs = new List<MoveLimb>();
    [SerializeField] private SpinTongue _tongue;
    [SerializeField] private Spit _spit;
    private Camera _mainCamera => Camera.main;
    private Coroutine[] legsMoving = new Coroutine[6];

    private bool _hasWhipAudioPlayed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // this is pretty wet, if there's time I'll come back to it later
    #region Input Listeners
    #region Left Rear
    private void OnLeftRearLegDown(InputValue value)
    {
        // start the coroutine
        legsMoving[(int)LimbNames.LeftRearLeg] = StartCoroutine(MoveLeg((int)LimbNames.LeftRearLeg));
    }
    private void OnLeftRearLegUp(InputValue value)
    {
        // stop the current coroutine
        StopCoroutine(legsMoving[(int)LimbNames.LeftRearLeg]);
        // try to stick the leg
        _limbs[(int)LimbNames.LeftRearLeg].TryToStick();
    }
    #endregion
    #region Right Rear
    private void OnRightRearLegDown(InputValue value)
    {
        legsMoving[(int)LimbNames.RightRearLeg] = StartCoroutine(MoveLeg((int)LimbNames.RightRearLeg));
    }
    private void OnRightRearLegUp(InputValue value)
    {
        StopCoroutine(legsMoving[(int)LimbNames.RightRearLeg]);
        _limbs[(int)LimbNames.RightRearLeg].TryToStick();
    }
    #endregion
    #region Left Front
    private void OnLeftFrontLegDown(InputValue value)
    {
        legsMoving[(int)LimbNames.LeftFrontLeg] = StartCoroutine(MoveLeg((int)LimbNames.LeftFrontLeg));
    }
    private void OnLeftFrontLegUp(InputValue value)
    {
        StopCoroutine(legsMoving[(int)LimbNames.LeftFrontLeg]);
        _limbs[(int)LimbNames.LeftFrontLeg].TryToStick();
    }
    #endregion
    #region Right Front
    private void OnRightFrontLegDown(InputValue value)
    {
        legsMoving[(int)LimbNames.RightFrontLeg] = StartCoroutine(MoveLeg((int)LimbNames.RightFrontLeg));
    }
    private void OnRightFrontLegUp(InputValue value)
    {
        StopCoroutine(legsMoving[(int)LimbNames.RightFrontLeg]);
        _limbs[(int)LimbNames.RightFrontLeg].TryToStick();
    }
    #endregion
    #region Neck
    private void OnNeckDown(InputValue value)
    {
        // start the coroutine
        legsMoving[(int)LimbNames.Neck] = StartCoroutine(MoveLeg((int)LimbNames.Neck));
    }
    private void OnNeckUp(InputValue value)
    {
        // stop the current coroutine
        StopCoroutine(legsMoving[(int)LimbNames.Neck]);
        // try to stick the leg
        _limbs[(int)LimbNames.Neck].TryToStick();
    }
    #endregion
    #region Tongue
    private void OnTongueDown(InputValue value)
    {
        _tongue.TryHover();
    }
    private void OnTongueUp(InputValue value)
    {
        _tongue.StopSpinning();
        _tongue.Recharge();
    }
    #endregion
    #region Release Limb
    private void OnReleaseLimbs()
    {
        foreach(MoveLimb limb in _limbs)
        {
            limb.ReleaseLimb();
        }
    }
    #endregion
    #region Spit
    private void OnSpit(InputValue value)
    {
        _spit.TryShoot();
    }
    #endregion
    #endregion

    private IEnumerator MoveLeg(int index)
    {
        _limbs[index].FreeLimb();
        PlayLimbSound();
        while (true)
        {
            yield return null;
            _limbs[index].MoveInDirection(MouseHelper.GetMouseWorldSpace(_mainCamera));
            _hasWhipAudioPlayed = false;
        }
    }

    private void PlayLimbSound()
    {
        if (!_hasWhipAudioPlayed)
        {
            AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.LlamaLegs, this.gameObject.transform.position);
            _hasWhipAudioPlayed = true;
        }
    }
}
