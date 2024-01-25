using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIToggle : MonoBehaviour
{
    //this bool checks if sound is supposed to be on
    private bool _soundIsOn = true;
    public void ToggleSound()
    {
        _soundIsOn = !_soundIsOn;

        // if sound is on
        if(_soundIsOn)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Sound On";
        }
        //if sound is off
        else
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Sound Off";
        }
    }
}
