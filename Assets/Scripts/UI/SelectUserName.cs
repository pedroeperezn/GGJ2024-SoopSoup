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
    [SerializeField] private TextMeshProUGUI _errorText;
    private Authorize _authorize = new Authorize();
    private string _orignalText = "";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        _authorize.AuthorizeAnonymousUserAsync();
        _input.text = _authorize.GetCurrentPlayerName();
        _orignalText = _errorText.text;
    }

    public async void TrySignIn()
    {
        if (string.IsNullOrEmpty(_input.text)) return;

        string newName = _input.text;
        
        try
        {
            _errorText.text = _orignalText;
            _errorText.gameObject.SetActive(false);
            await _authorize.SetPlayerName(newName);
            SceneManager.LoadScene(_nextScene);
        }
        catch(System.Exception e)
        {
            Debug.Log("failed to sign in");
            _errorText.gameObject.SetActive(true);
            _errorText.text += $"\n({e.Message})";
        }
    }
}
