using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomLlamaSound : MonoBehaviour
{
    [SerializeField] private float minCollisionVelocity = 50f;

    private bool _isReacPlaying;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > minCollisionVelocity && !_isReacPlaying) 
        {
            AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.LlamaInPainReac, this.gameObject.transform.position);
            _isReacPlaying = true;
            StartCoroutine(ToggleReactPlaying());
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

            if(!_isReacPlaying) 
            {
                AudioManager.Instance.PlayOneShot(FMODEventsManager.Instance.LlamaCasualReac, this.gameObject.transform.position);
                _isReacPlaying = true;
                StartCoroutine(ToggleReactPlaying());
            }
        }
        
    }

    private IEnumerator ToggleReactPlaying() 
    {
        while (true)
        {
            float waitTime = 3f;
            yield return new WaitForSeconds(waitTime);
            _isReacPlaying = false;
        }
    }

}
