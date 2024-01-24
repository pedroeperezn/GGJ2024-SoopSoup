// Copyright (C) 2024 Soop Soup
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    //loads scene when called
    public void LoadScene(string sceneName)
    {
        //calls the scene using scene name indicated
        SceneManager.LoadScene(sceneName);
    }

    // quits the game when called
    public void QuitGame()
    {
        //if it's built, this will quit the game
        Application.Quit();

        //if we're testing through editor, this will quit the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
