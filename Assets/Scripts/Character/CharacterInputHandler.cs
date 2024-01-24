using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField] private float _strength = 1000;
    [SerializeField] private List<MoveLimb> _limbs = new List<MoveLimb>();
    private Vector2 _mousePositionScreenSpace = Vector2.zero;
    private Camera _mainCamera => Camera.main;
    private bool[] _moving = new bool[] { false, false, false, false, false, false };
    private IEnumerator moveLeg;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Right"), LayerMask.NameToLayer("Left"));
    }

    // this is pretty wet, if there's time I'll come back to it later
    #region Input Listeners
    #region Left Rear
    private void OnLeftRearLegDown(InputValue value)
    {
        // check to see if we're moving any of the legs, if we are stop
        if (Array.IndexOf(_moving, true) != -1) return;
        // define the coroutine, this is so we can easily stop later
        moveLeg = MoveLeg((int)LimbNames.LeftRearLeg);
        // start the coroutine
        StartCoroutine(moveLeg);
    }
    private void OnLeftRearLegUp(InputValue value)
    {
        // set the correct moving bool false
        _moving[(int)LimbNames.LeftRearLeg] = false;
        // stop the current coroutine
        StopCoroutine(moveLeg);
        // try to stick the leg
        _limbs[(int)LimbNames.LeftRearLeg].TryToStick();
    }
    #endregion
    #region Right Rear
    private void OnRightRearLegDown(InputValue value)
    {
        if (Array.IndexOf(_moving, true) != -1) return;
        moveLeg = MoveLeg((int)LimbNames.RightRearLeg);
        StartCoroutine(moveLeg);
    }
    private void OnRightRearLegUp(InputValue value)
    {
        _moving[(int)LimbNames.RightRearLeg] = false;
        StopCoroutine(moveLeg);
        _limbs[(int)LimbNames.RightRearLeg].TryToStick();
    }
    #endregion
    #region Left Front
    private void OnLeftFrontLegDown(InputValue value)
    {
        if (Array.IndexOf(_moving, true) != -1) return;
        moveLeg = MoveLeg((int)LimbNames.LeftFrontLeg);
        StartCoroutine(moveLeg);
    }
    private void OnLeftFrontLegUp(InputValue value)
    {
        _moving[(int)LimbNames.LeftFrontLeg] = false;
        StopCoroutine(moveLeg);
        _limbs[(int)LimbNames.LeftFrontLeg].TryToStick();
    }
    #endregion
    #region Right Front
    private void OnRightFrontLegDown(InputValue value)
    {
        if (Array.IndexOf(_moving, true) != -1) return;
        moveLeg = MoveLeg((int)LimbNames.RightFrontLeg);
        StartCoroutine(moveLeg);
    }
    private void OnRightFrontLegUp(InputValue value)
    {
        _moving[(int)LimbNames.RightFrontLeg] = false;
        StopCoroutine(moveLeg);
        _limbs[(int)LimbNames.RightFrontLeg].TryToStick();
    }
    #endregion
    #region Neck
    private void OnNeckDown(InputValue value)
    {
        // check to see if we're moving any of the legs, if we are stop
        if (Array.IndexOf(_moving, true) != -1) return;
        // define the coroutine, this is so we can easily stop later
        moveLeg = MoveLeg((int)LimbNames.Neck);
        // start the coroutine
        StartCoroutine(moveLeg);
    }
    private void OnNeckUp(InputValue value)
    {
        // set the correct moving bool false
        _moving[(int)LimbNames.Neck] = false;
        // stop the current coroutine
        StopCoroutine(moveLeg);
        // try to stick the leg
        _limbs[(int)LimbNames.Neck].TryToStick();
    }
    #endregion
    #endregion

    private IEnumerator MoveLeg(int index)
    {
        _moving[index] = true;
        while (true)
        {
            yield return null;
            _mousePositionScreenSpace = Input.mousePosition;
            Vector2 mousePositionWorldSpace = _mainCamera.ScreenToWorldPoint(new Vector3(_mousePositionScreenSpace.x, _mousePositionScreenSpace.y, 0));
            //TODO: get the world space coordinates
            _limbs[index].MoveInDirection(mousePositionWorldSpace, _strength);
        }
    }

}
