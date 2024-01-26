using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this guy's just full entirely of functions that get called by the trigger event
[CreateAssetMenu]
public class Checkpoint : ScriptableObject
{
    [SerializeField] private GameObject[] _tourists;
    [SerializeField] private float _expectedCompletionTime = 10;

    // Music Change can go here or Pedro can use the event itself
    public void ChangeMusic()
    {
        Debug.Log("Music Changed");
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
    public void PopUpUI()
    {
        Debug.Log("Popped up UI");
    }

    public void GenerateScore()
    {
        ScoreManager score = FindObjectOfType<ScoreManager>();
        score.CalculateTimeBonus(_expectedCompletionTime);
        score.CalculateChaosBonus();
        score.CalculatePeopleBonus();
    }
}
