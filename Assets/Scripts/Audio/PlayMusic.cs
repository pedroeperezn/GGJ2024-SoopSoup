using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public static PlayMusic Instance;

    [SerializeField] private bool _isMainMenu;

    private EventInstance MusicInstance;
    private int _currentLevel;

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
        if (!_isMainMenu)
        {
            MusicInstance = AudioManager.Instance.CreateInstance(FMODEventsManager.Instance.GameMusic);
            PLAYBACK_STATE playbackState;
            MusicInstance.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                StartMusic();
            }

            else
            {
                MusicInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
        else 
        {
            MusicInstance = AudioManager.Instance.CreateInstance(FMODEventsManager.Instance.MainMenuMusic);
            PLAYBACK_STATE playbackState;
            MusicInstance.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                StartMusic();
            }

            else
            {
                MusicInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }

    private void StopMusic() 
    {
        MusicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

    private void StartMusic() 
    {
        MusicInstance.start();
    }
    
    public void PlayLevelMusic()
    {
        MusicInstance.setParameterByName("CurrentLevel", _currentLevel);
        MusicInstance.setParameterByName("Continue", 1);
        MusicInstance.setParameterByName("LevelFinished", 0);
    }

    public void LevelCompleteMusic() 
    {
        
        MusicInstance.setParameterByName("LevelFinished", 1);
        MusicInstance.setParameterByName("Continue", 0);
    }

    private void OnDestroy()
    {
        MusicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void SetCurrentMusicLevel(int newLevel) 
    {
        _currentLevel = newLevel;
    }

}
