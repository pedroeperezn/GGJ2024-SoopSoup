using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    [SerializeField] private string type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Llama")
        {
            TutorialHandler handler = FindObjectOfType<TutorialHandler>();

            if (type.Equals("spit"))
                handler.StartSpit();
            else if (type.Equals("fly"))
                handler.StartFly();
            else if (type.Equals("tourist"))
                handler.StartTourist();
        }
    }
}
