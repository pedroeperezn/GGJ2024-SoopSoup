using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TestLeaderboard : MonoBehaviour
{
    [SerializeField] private string _playerName = "Connor";
    [SerializeField] private int _score = 100;

    Leaderboard _leaderBoard => GetComponent<Leaderboard>();

    [ContextMenu("Post a score")]
    public async void PostAScore()
    {
        LeaderBoardPlace place = await _leaderBoard.PostScores(LeaderBoardIds.LeaderBoards[4], _playerName, _score);
        Debug.Log($"{place.Name} scored a {place.Score} which earned them a rank of {place.Rank}");
    }

    [ContextMenu("Get a scores")]
    public async void GetTheScores()
    {
        LeaderBoardPlace[] places = await _leaderBoard.GetScores(LeaderBoardIds.LeaderBoards[4]);
        foreach(LeaderBoardPlace place in places)
        {
            Debug.Log($"{place.Name} scored a {place.Score} which earned them a rank of {place.Rank}");
        }
    }
}