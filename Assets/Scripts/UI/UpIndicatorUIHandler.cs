using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpIndicatorUIHandler : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private AnimationCurve _fillCurve;
    private SpinTongue _spinTongue => FindAnyObjectByType<SpinTongue>();
    private Coroutine _currentTask;

    private void Start()
    {
        if (!_spinTongue) return;

        _spinTongue.StartRecharge.AddListener(StartFilling);
        _spinTongue.StartHover.AddListener(StartDraining);
    }

    private void StartFilling()
    {
        if (_currentTask != null) StopCoroutine(_currentTask);
        _currentTask = StartCoroutine(Fill());
    }
    private void StartDraining()
    {
        if (_currentTask != null) StopCoroutine(_currentTask);
        _currentTask = StartCoroutine(Drain());
    }
    private IEnumerator Fill()
    {
        if (!_spinTongue) yield return null;
        Debug.Log("Start Filling");
        while (_spinTongue.CurrentTime < _spinTongue.CoolDownTime)
        {
            yield return null;
            float percentComplete = _spinTongue.CurrentTime / _spinTongue.CoolDownTime;
            _image.fillAmount = _fillCurve.Evaluate(percentComplete);
        }
    }

    private IEnumerator Drain()
    {
        if (!_spinTongue) yield return null;
        Debug.Log("Start Draining");
        while (_spinTongue.CurrentTime < _spinTongue.MaxSpinTime)
        {
            yield return null;
            float percentDeplete = _spinTongue.CurrentTime / _spinTongue.MaxSpinTime;
            _image.fillAmount = 1 - _fillCurve.Evaluate(percentDeplete);
        }
    }
}
