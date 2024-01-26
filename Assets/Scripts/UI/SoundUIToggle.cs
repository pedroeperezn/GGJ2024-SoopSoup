using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIToggle : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [Header("Image")]
    [SerializeField] private Image _image;
    //this bool checks if sound is supposed to be on
    private bool _soundIsOn = true;
    public void ToggleSound()
    {
        Debug.Log("button pressed");
        _soundIsOn = !_soundIsOn;

        // if sound is on
        if(_soundIsOn)
        {
            _image.sprite = _soundOn;
        }
        //if sound is off
        else
        {
            _image.sprite = _soundOff;
        }
    }
}
