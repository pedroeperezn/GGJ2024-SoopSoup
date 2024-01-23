using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEventsManager : MonoBehaviour
{
    public static FMODEventsManager Instance { get; private set; }

    [field: Header("Llama test SFX")]
    [field: SerializeField] public EventReference LlamaTest { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one audio manager");
        }

        Instance = this;
    }

}
