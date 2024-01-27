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
    private string _leaderboardId = "test-id";
    private Authorize _authorize = new Authorize();

    private async void Awake() 
    {
        await UnityServices.InitializeAsync();
        _authorize.AuthorizeAnonymousUserAsync("Connor");
    }

    [ContextMenu("Get Scores")]
    public async void GetScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(_leaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    [ContextMenu("Post Score")]
    public async void PostScores()
    {
        try
        {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(_leaderboardId, 103);
            Debug.Log($"Posted {103} to {_leaderboardId}");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
