using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField] private List<MoveLimb> _limbs = new List<MoveLimb>();
    private Vector2 _mousePositionScreenSpace = Vector2.zero;
    private Camera _mainCamera => Camera.main;

    private void Update()
    {
        _mousePositionScreenSpace = Input.mousePosition;
    }

    private void OnMovement(InputValue value)
    {
        Vector3 input = value.Get<Vector3>();
        int index = ReadInput(input);
        Vector2 target = _mainCamera.ScreenToWorldPoint(new Vector3(_mousePositionScreenSpace.x, _mousePositionScreenSpace.y, 0));
        Debug.Log(index);
    }

    private int ReadInput(Vector3 input)
    {
        if (!InputLookupTable.InputToLimbNames.ContainsKey(input)) return 0;
        return (int)InputLookupTable.InputToLimbNames[input];
    }
}
