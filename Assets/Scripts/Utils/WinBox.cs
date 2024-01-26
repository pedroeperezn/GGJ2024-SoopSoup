using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Checkpoints/Win")]
public class WinBox : ScriptableObject
{
    public void PopUpUI()
    {
        // Micy deez might want to use this to pop up win ui
    }

    public void FreezeTime()
    {
        Time.timeScale = 0;
    }

    public void PlayWinSFX()
    {
        // Pedro might use this to play an audio cue
    }

    public void PostToLeaderboard()
    {
        // this guy will post the players score to the leaderboard
    }
}
