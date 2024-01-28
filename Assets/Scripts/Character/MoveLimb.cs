using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(HingeJoint2D))]
public class MoveLimb : MonoBehaviour
{
    [SerializeField] private LimbNames _limbName;
    [SerializeField] private float _grabRadius = 1.5f;
    [SerializeField] private Color _canGrabColor, _canNotGrabColor, _stuckColor;

    private Rigidbody2D _rb;
    private SpringJoint2D _spring;
    private ManageBodyWeight _body;

    private Collider2D[] _objectsInRange;

    private bool _canGrab = false;

    private LineRenderer _grabCircle;
    private int _segments = 50;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
        FindBody(GetComponent<HingeJoint2D>()).TryGetComponent(out _body);
        if (_body == null) Debug.LogError($"Body not found {gameObject.name}");

        _grabCircle = GetComponent<LineRenderer>();
        // set color
        _grabCircle.startColor = _canNotGrabColor;
        _grabCircle.endColor = _canNotGrabColor;

        // make it a circle
        _grabCircle.positionCount = _segments + 1;
        float x;
        float y;
        float angle = 20f;
        for (int i = 0; i < (_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _grabRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _grabRadius;

            _grabCircle.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / _segments);
        }
    }

    // ugly very bad, set color properly based off it's state
    private void FixedUpdate()
    {
        _objectsInRange = Physics2D.OverlapCircleAll(transform.position, _grabRadius, LayerMask.GetMask("Ground"));

        if (_objectsInRange.Length > 0 && _rb.bodyType == RigidbodyType2D.Dynamic)
        {
            _grabCircle.startColor = _canGrabColor;
            _grabCircle.endColor = _canGrabColor;
            _canGrab = true;
            return;
        }
        else
        {
            _grabCircle.startColor = (_rb.bodyType == RigidbodyType2D.Dynamic) ? _canNotGrabColor : _stuckColor;
            _grabCircle.endColor = (_rb.bodyType == RigidbodyType2D.Dynamic) ? _canNotGrabColor : _stuckColor;
        }

        _canGrab = false;
    }

    private GameObject FindBody(HingeJoint2D hinge)
    {
        Rigidbody2D connectedBody = hinge.connectedBody;
        if (connectedBody == null || !connectedBody.TryGetComponent(out HingeJoint2D newHinge)) return connectedBody.gameObject;
        return FindBody(newHinge);
    }

    public void FreeLimb()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _spring.enabled = true;

        ++_body.CurrentlyMovingLimbCount;
        _body.IsSticking[(int)_limbName] = false;
        _body.ManageLimbWeight(_rb);
    }
 
    public void MoveInDirection(Vector2 targetInWorldSpace)
    {
        // AUDIO FOR 'SWOOSH' MIGHT GO HERE, I'm honestly not sure how you want to achieve it,
        // feel free to message Connor if this wont work or if it's confusing
        _spring.connectedAnchor = targetInWorldSpace;
    }

    public void TryToStick()
    {
        //disable the spring and check to see if we're near a stick-able surface, if we are freeze the rb else just flop
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Llama"), gameObject.layer, false);
        _spring.enabled = false;
        if (_canGrab)
        {
            // AUDIO GOES HERE FOR STICKING A LIMB
            AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.LlamaSticks, this.gameObject.transform.position);
            _grabCircle.startColor = _stuckColor;
            _grabCircle.endColor = _stuckColor;
            _rb.bodyType = RigidbodyType2D.Static;
            _body.IsSticking[(int)_limbName] = true;
        }
        --_body.CurrentlyMovingLimbCount;
        _body.ManageLimbWeight(_rb);
    }

    public void ReleaseLimb()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _spring.enabled = false;
        _body.IsSticking[(int)_limbName] = false;
        _body.CurrentlyMovingLimbCount = 0;
        _body.ManageLimbWeight(_rb);
    }
}
