using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour
{
    private Authorize _authorize = new Authorize();

    private async void Awake() 
    {
        await UnityServices.InitializeAsync();
        _authorize.AuthorizeAnonymousUserAsync();
    }

    [ContextMenu("Get Scores")]
    public async Task<LeaderBoardPlace[]> GetScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderBoardIds.LeaderBoards[3]);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        Debug.Log(scoresResponse.Results[0].Score);
        LeaderBoardPlace[] output = new LeaderBoardPlace[scoresResponse.Results.Count];
        for(int i = 0; i < scoresResponse.Results.Count; ++i)
        {
            output[i].Name = scoresResponse.Results[i].PlayerName;
            output[i].Rank = scoresResponse.Results[i].Rank;
            output[i].Score = (int)scoresResponse.Results[i].Score;
        }
        return output;
    }

    public async void PostScores(string leaderboardId, string playerName, int score)
    {
        try
        {
            await _authorize.SetPlayerName(playerName);
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
            Debug.Log($"Posted {score} to {leaderboardId}");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
