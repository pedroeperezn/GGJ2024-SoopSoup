using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmiters;

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


}
