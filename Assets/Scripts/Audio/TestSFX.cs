using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSFX : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))  
        {
            AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.LlamaTest,this.transform.position);
        }
    }
}
