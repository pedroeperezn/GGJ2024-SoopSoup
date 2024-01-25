// Copyright (C) 2024 Soop Soup
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class FollowScript : MonoBehaviour
{
    [SerializeField] private GameObject _followThisObject;

    private void Update()
    {
        //make sure position = position of thing we are following
        Vector3 pos = new Vector3(_followThisObject.transform.position.x, 
            _followThisObject.transform.position.y, -1.3f);
        this.transform.position = _followThisObject.transform.position;

        //TestOnMouse();
    }

    private void TestOnMouse()
    {
        //test using mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // Set the distance from the camera to the canvas

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldMousePos;

        Debug.Log($"x pos: {mousePos.x} y pos: {mousePos.y}");
    }
}
