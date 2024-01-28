using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Checkpoints/Win")]
public class WinBox : ScriptableObject
{
    public void PlayWinSFX()
    {
        // Pedro might use this to play an audio cue
    }

    public async void PostToLeaderboard()
    {
        // this guy will post the players score to the leaderboard
        Leaderboard leaderboard = FindObjectOfType<Leaderboard>();
        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        Authorize _autho = new Authorize();

        await leaderboard.PostScores(LeaderBoardIds.LeaderBoards[3], _autho.GetCurrentPlayerName(), scoreManager.Score);
    }
}
