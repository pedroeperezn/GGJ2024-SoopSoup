using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this guy's just full entirely of functions that get called by the trigger event
[CreateAssetMenu(menuName = "ScriptableObjects/Checkpoints/Checkpoint")]
public class Checkpoint : ScriptableObject
{
    [SerializeField] private GameObject[] _tourists;
    [SerializeField] private float _expectedCompletionTime = 10;
    [SerializeField] private RectTransform _levelComplete;

    // Music Change can go here or Pedro can use the event itself
    public void ChangeMusic()
    {
        PlayMusic.Instance.LevelCompleteMusic();
    }

    // idk if we're gonna use this but it seems like it would be useful
    public void AddMoreTourist(Transform _spawnPosition)
    {
        for(int i = 0; i < _tourists.Length; i++)
        {
            Instantiate(_tourists[i], _spawnPosition);
        }
    }

    // Micy deez might want to use this, or she can just use the event by itself
    public void PopUpUI(bool gameDone)
    {
        Debug.Log("Popped up UI");

        LevelCompleteHandler lvlCompleteUI = FindAnyObjectByType<LevelCompleteHandler>();

        if(gameDone)
            lvlCompleteUI.EndGame();
        else
            lvlCompleteUI.CheckpointReached();
    }

    public void GenerateScore()
    {
        ScoreManager score = FindObjectOfType<ScoreManager>();
        score.CalculateTimeBonus(_expectedCompletionTime);
        score.CalculateChaosBonus();
        score.CalculatePeopleBonus();
        Debug.Log(score.Score);
    }
}
