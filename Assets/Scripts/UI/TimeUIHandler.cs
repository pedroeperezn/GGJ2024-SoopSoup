using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private ScoreManager _scoreManager;

    private void Update()
    {
        int minutes = (int)(_scoreManager.TimeTaken / 60);
        int seconds = (int)(_scoreManager.TimeTaken % 60);
        _time.text = $"{minutes}:{seconds}";
    }
}
