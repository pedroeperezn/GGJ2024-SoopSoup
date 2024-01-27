using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Leaderboards;

public class Leaderboard : MonoBehaviour
{
    private Authorize _authorize = new Authorize();

    private async void Awake() 
    {
        await UnityServices.InitializeAsync();
        _authorize.AuthorizeAnonymousUserAsync();
    }

    /// <summary>
    /// gets the score
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<LeaderBoardPlace[]> GetScores(string leaderBoardId, int limit = 10)
    {
        // returns that yucky json we are too familiar with
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(leaderBoardId, new GetPlayerRangeOptions 
        {
            RangeLimit = limit
        });

        // create output by converting yucky JSON into our nice leaderboardplace
        LeaderBoardPlace[] output = new LeaderBoardPlace[scoresResponse.Results.Count];
        for(int i = 0; i < scoresResponse.Results.Count; ++i)
        {
            output[i].Name = scoresResponse.Results[i].PlayerName;
            output[i].Rank = scoresResponse.Results[i].Rank;
            output[i].Score = (int)scoresResponse.Results[i].Score;
        }
        return output;
    }

    public async Task<LeaderBoardPlace> PostScores(string leaderboardId, string playerName, int score)
    {
        LeaderBoardPlace output;
        try
        {
            await _authorize.SetPlayerName(playerName);
            var result = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);

            output.Name = result.PlayerName;
            output.Rank = result.Rank;
            output.Score = (int)result.Score;

            Debug.Log($"Posted {score} to {leaderboardId}");
            return output;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        throw new Exception("Failed to post");
    }
}
