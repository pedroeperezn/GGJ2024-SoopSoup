using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public static PlayMusic Instance;
    private EventInstance musicInstance;

    private void Awake()
    {
        if(Instance != null) 
        {
            Debug.Log("More than one Music Manager");
        }

        Instance = this;
    }

    private void Start()
    {
        musicInstance = AudioManager.Instance.CreateInstance(FMODEventsManager.Instance.Music);
        PLAYBACK_STATE playbackState;
        musicInstance.getPlaybackState(out playbackState);

        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            StartMusic();
        }

        else 
        {
            musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void StopMusic() 
    {
        musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

    private void StartMusic() 
    {
        musicInstance.start();
    }

    public void ChangeLevelMusic(int NewLevel)
    {
        //StopMusic();
        Debug.Log("Trigger Exit");
        musicInstance.setParameterByName("CurrentLevel", NewLevel);
        musicInstance.setParameterByName("Continue", 1);
        musicInstance.setParameterByName("LevelFinished", 0);
    }

    public void LevelCompleteMusic() 
    {
        
        musicInstance.setParameterByName("LevelFinished", 1);
        musicInstance.setParameterByName("Continue", 0);
    }

    private void OnDestroy()
    {
        musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

}
