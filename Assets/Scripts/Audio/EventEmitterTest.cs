using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]
public class EventEmitterTest : MonoBehaviour
{
    private StudioEventEmitter emitter;

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
    }

    private void OnDestroy()
    {
        emitter.Stop();
    }
}
