using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEventsManager : MonoBehaviour
{
    public static FMODEventsManager Instance { get; private set; }

    [field: Header("Game Music")]
    [field: SerializeField] public EventReference Music { get; private set; }


    [field: Header("Llama SFX")]
    [field: SerializeField] public EventReference LlamaReacs { get; private set; }
    [field: SerializeField] public EventReference LlamaLegs { get; private set; }
    [field: SerializeField] public EventReference LlamaSticks { get; private set; }
    [field: SerializeField] public EventReference LlamaSpits { get; private set; }
    [field: SerializeField] public EventReference LlamaHelicopter { get; private set; }



    [field: Header("Emitter Test")]
    [field: SerializeField] public EventReference EmitterTest { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference Ambience { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one audio manager");
        }

        Instance = this;
    }

}
