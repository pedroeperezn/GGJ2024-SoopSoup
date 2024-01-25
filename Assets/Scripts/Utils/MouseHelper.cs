using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseHelper
{
    public static Vector2 GetMouseWorldSpace(Camera cam)
    {
        Vector2 _mousePositionScreenSpace = Input.mousePosition;
        return cam.ScreenToWorldPoint(new Vector3(_mousePositionScreenSpace.x, _mousePositionScreenSpace.y, 0));
    }
}
