using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourist : MonoBehaviour
{
    [SerializeField] private float _grabRadius = 20f;
    [SerializeField] private Rigidbody2D _body;
    private List<SpringJoint2D> _joints = new List<SpringJoint2D>();

    private void TryGrab()
    {
        Collider2D[] inRange = Physics2D.OverlapCircleAll(_body.position, _grabRadius, LayerMask.GetMask("Human"));
        if (inRange.Length == 0) return;
        _joints.Clear();
        foreach (Collider2D collider in inRange)
        {
            if (collider.TryGetComponent(out SpringJoint2D newJoint))
            {
                _joints.Add(newJoint);
            }
        }

        bool synced = true;
        foreach (SpringJoint2D joint in _joints)
        {
            if (!joint.enabled)
            {
                synced = false;
            }
        }

        foreach (SpringJoint2D joint in _joints)
        {
            joint.connectedBody = _body;
            joint.enabled = (synced) ? !joint.enabled : true;
        }
    }

    private void OnGrabTourist()
    {
        TryGrab();
    }
}
