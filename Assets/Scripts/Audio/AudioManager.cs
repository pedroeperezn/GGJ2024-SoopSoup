using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one audio manager");
        }
        
        Instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition) 
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }


}
