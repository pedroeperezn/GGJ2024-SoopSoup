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

    private Coroutine[] legsMoving = new Coroutine[6];

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Right"), LayerMask.NameToLayer("Left"));
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
    #endregion

    private IEnumerator MoveLeg(int index)
    {
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
