using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hey so what I ended up doing here is firing some events from the spin tongue and using coroutines, 
/// keeping track of the state so that if the player lets go in the middle of a hover we can stop the 
/// drain coroutine and start the fill one without having to worry, we could if we wanted change it 
/// so that the fill starts at where the drain left it, but honestly I'm being pretty lazy and I think 
/// it looks fine for now
/// </summary>
public class UpIndicatorUIHandler : MonoBehaviour
{
    // grab the image and animation curve cause easy
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
        while (_spinTongue.CurrentTime < _spinTongue.MaxSpinTime)
        {
            yield return null;
            float percentDeplete = _spinTongue.CurrentTime / _spinTongue.MaxSpinTime;
            _image.fillAmount = 1 - _fillCurve.Evaluate(percentDeplete);
        }
    }
}
