using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

public class Authorize
{
    /// <summary>
    /// returns the player's name
    /// </summary>
    /// <returns></returns>
    public string GetCurrentPlayerName()
    {
        string name = AuthenticationService.Instance.PlayerName;
        if (string.IsNullOrEmpty(name)) return "undefined";
        return TruncatePlayerName(name, '#');
    }

    public async Task<string> SetPlayerName(string newName)
    {
        return await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);
    }

    /// <summary>
    /// Signs in anonymously and assigns a name to the player
    /// </summary>
    /// <param name="playerName">The name you wish to assign to the player</param>
    public async void AuthorizeAnonymousUserAsync()
    {
        if (AuthenticationService.Instance.IsSignedIn) return;

        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private string TruncatePlayerName(string playerName, char delimiter)
    {
        int delimiterIndex = playerName.IndexOf(delimiter);
        string cleanName = delimiterIndex > -1 ? playerName.Substring(0, delimiterIndex) : playerName;

        // Truncate the name to a maximum of 10 characters and append an ellipsis if it was longer
        const int maxLength = 10;
        if (cleanName.Length > maxLength)
        {
            cleanName = cleanName.Substring(0, maxLength) + "...";
        }

        return cleanName;
    }
}