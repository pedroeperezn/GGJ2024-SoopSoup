using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private EventInstance musicInstance;
    // Start is called before the first frame update
    private void Start()
    {
        musicInstance = AudioManager.Instance.CreateInstance(FMODEventsManager.Instance.Music);

        PLAYBACK_STATE playbackState;
        musicInstance.getPlaybackState(out playbackState);

        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            musicInstance.start();
        }

        else 
        {
            musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }


    private void OnDestroy()
    {
        musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

}
