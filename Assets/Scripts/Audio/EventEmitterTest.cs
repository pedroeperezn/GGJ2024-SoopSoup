using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]
public class EventEmitterTest : MonoBehaviour
{
    private StudioEventEmitter emitter;
    private float counter;

    private void Start()
    {
        emitter = AudioManager.Instance.InitStudioEventEmitter(FMODEventsManager.Instance.EmitterTest,this.gameObject);
        emitter.Play();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) 
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            counter += 0.1f;
            AudioManager.Instance.SetAmbienceParameter("WindIntensity", counter);
        }
    }

    private void OnDestroy()
    {
        emitter.Stop();
    }
}
