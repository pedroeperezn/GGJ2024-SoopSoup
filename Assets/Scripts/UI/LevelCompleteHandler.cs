using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteHandler : MonoBehaviour
{
    [SerializeField] private GameObject _checkpointScreen;
    [SerializeField] private GameObject _gameEndScreen;
    public void CheckpointReached()
    {
        // pause time & popup UI
        _gameEndScreen.SetActive(false);
        _checkpointScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ContinueLevel()
    {
        // unpause time and disable UI
        Time.timeScale = 1f;
        _checkpointScreen.SetActive(false);
    }

    public void EndGame()
    {
        this.gameObject.SetActive(true);
        _gameEndScreen.SetActive(true);
        _checkpointScreen.SetActive(false);
    }
}
