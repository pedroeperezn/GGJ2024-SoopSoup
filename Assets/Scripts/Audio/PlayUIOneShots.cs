using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIOneShots : MonoBehaviour
{
    public void PlayOnHoverButtonSound() 
    {
        AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.OnHoverButton,transform.position);
    }

    public void PlayOnClickButtonSound() 
    {
        AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.OnClickButton,transform.position);
    }
}
