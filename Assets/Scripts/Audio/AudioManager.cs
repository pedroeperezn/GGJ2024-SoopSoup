using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmiters;
    private EventInstance ambienceEventInstance;
    private bool _isMuted;

   public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one audio manager");
        }
        
        Instance = this;
        eventInstances = new List<EventInstance>();
        eventEmiters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeAmbience(FMODEventsManager.Instance.Ambience);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition) 
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance CreateInstance(EventReference sound) 
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(sound);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitStudioEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    { 
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmiters.Add(emitter);
        return emitter;
    
    }

    private void InitializeAmbience(EventReference ambienceEventReference) 
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void SetAmbienceParameter(string parameterName, float parameterValue)
    { 
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }

    private void OnDestroy()
    {
        if(eventInstances.Count > 0) 
        {
            foreach (EventInstance eventInstance in eventInstances) 
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
        }

        if(eventEmiters.Count > 0)
        {
            foreach (StudioEventEmitter emitter in eventEmiters) 
            {
                emitter.Stop();
            }
        }
    }

    public void ToggleMuteSound() 
    {
        _isMuted = !_isMuted;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsMuted", Convert.ToInt32(_isMuted));
    }


}
