using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField] private float _strength = 1000;
    [SerializeField] private List<MoveLimb> _limbs = new List<MoveLimb>();
    private Vector2 _mousePositionScreenSpace = Vector2.zero;
    private Camera _mainCamera => Camera.main;
    private bool _moving = false;
    private IEnumerator moveLeg;

    #region input listeners
    private void OnLeftRearLegDown(InputValue value)
    {
        if (_moving) return;
        moveLeg = MoveLeg((int)LimbNames.LeftRearLeg);
        //TODO: get the world space coordinates
        StartCoroutine(moveLeg);
    }
    private void OnLeftRearLegUp(InputValue value)
    {
        _moving = false;
        //TODO: get the world space coordinates
        StopCoroutine(moveLeg);
        _limbs[(int)LimbNames.LeftRearLeg].Stick();
    }
    #endregion

    private IEnumerator MoveLeg(int index)
    {
        _moving = true;
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
