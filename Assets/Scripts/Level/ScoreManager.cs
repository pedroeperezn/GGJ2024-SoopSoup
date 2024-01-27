using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugText;
    [SerializeField] private float _scoreModifier = 10;
    
    public int Score => _score;

    internal int PeopleCount = 0;
    
    private int _chaosCounter = 0;
    private float _timeTaken = 0;

    private int _score;

    private void Awake()
    {
        StartCoroutine(CountTime());
    }
    private IEnumerator CountTime()
    {
        while(true)
        {
            yield return null;
            _timeTaken += Time.deltaTime;
        }
    }
    public void CalculateTimeBonus(float expectedTime)
    {
        Debug.Log(_timeTaken);
        float difference = expectedTime - _timeTaken;
        _timeTaken = 0;
        _score += Mathf.RoundToInt(Mathf.Clamp(difference, 0, Mathf.Infinity) * _scoreModifier);
    }
    public void CalculateChaosBonus()
    {
        _score += Mathf.RoundToInt(_chaosCounter * _scoreModifier);
    }
    public void IncrementChaos()
    {
        ++_chaosCounter;
    }
    public void CalculatePeopleBonus()
    {
        _score += Mathf.RoundToInt(PeopleCount * _scoreModifier);
    }
    private void Update()
    {
        if (_debugText) _debugText.text = _score.ToString();
    }
}
