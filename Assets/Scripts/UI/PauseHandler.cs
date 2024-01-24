// Copyright (C) 2024 Soop Soup
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    //pauses the game
    public void PauseGame()
    {
        //set time scale to 0, this pauses the game
        Time.timeScale = 0.0f; 
    }

    //resumes the game
    public void ResumeGame()
    {
        //set time scale to 1, this resumes the game
        Time.timeScale = 1.0f;
    }
}
