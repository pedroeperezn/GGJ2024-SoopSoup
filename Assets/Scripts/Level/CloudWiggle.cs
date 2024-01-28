using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWiggle : MonoBehaviour
{
    [SerializeField] private float _wiggleSpeed = 10;
    [SerializeField] private AnimationCurve _curve;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            StartCoroutine(Wiggle(transform.GetChild(i)));
        }
    }

    IEnumerator Wiggle(Transform childTransform)
    {
        yield return new WaitForSeconds(Random.Range(0f, 5f));
        float index = Random.Range(_curve.keys[_curve.length - 1].time, _curve.keys[0].time);
        bool goLeft = false;
        while (true)
        {
            yield return null;
            childTransform.Translate(Vector3.left * _curve.Evaluate(index) * _wiggleSpeed * Time.deltaTime);
            if (index > _curve.keys[_curve.length - 1].time || index < _curve.keys[0].time)
            {
                goLeft = !goLeft;
            }
            index = (goLeft) ? index + Time.deltaTime : index - Time.deltaTime;
        }
    }
}
