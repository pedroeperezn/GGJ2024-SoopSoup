using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomLlamaSound : MonoBehaviour
{
    [SerializeField] private float minCollisionVelocity = 50f;

    private bool _isReacPlaying;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > minCollisionVelocity) 
        {
            AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.LlamaInPainReac, this.gameObject.transform.position);
        }
    }

    private void Start()
    {
        StartCoroutine(PlayLlamaReactionSoundOnRandomInterval());
    }

    private IEnumerator PlayLlamaReactionSoundOnRandomInterval() 
    {
        while(true) 
        {
            float waitTime = Random.Range(15f, 30f);
            yield return new WaitForSeconds(waitTime);

            
            AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.LlamaCasualReac, this.gameObject.transform.position);
        }
        
    }

}
