using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectUserName : MonoBehaviour
{
    [SerializeField] private string _nextScene = "";
    [SerializeField] private TMP_InputField _input;
    private Authorize _authorize = new Authorize();

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        _authorize.AuthorizeAnonymousUserAsync();
        _input.text = _authorize.GetCurrentPlayerName();
    }

    public async void TrySignIn()
    {
        if (string.IsNullOrEmpty(_input.text)) return;

        string newName = _input.text;
        
        try
        {
            await _authorize.SetPlayerName(newName);
            SceneManager.LoadScene(_nextScene);
        }
        catch
        {
            Debug.Log("failed to sign in");
        }
    }
}
