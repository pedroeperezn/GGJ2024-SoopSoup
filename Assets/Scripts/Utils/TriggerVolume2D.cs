using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerVolume2D : MonoBehaviour
{
    [SerializeField] private string _tagFilter = "Player";
    [SerializeField] private bool _doOnce = true;
    [field: SerializeField] public bool Done { get; set; }

    public UnityEvent<GameObject> OnEnter;
    public UnityEvent<GameObject> OnExit;


    // ensure box collider is added, and is a trigger
    private void OnValidate()
    {
        if (TryGetComponent(out BoxCollider2D collider))
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckForTagMatch(other.gameObject) && (!_doOnce || !Done))
        {
            OnEnter.Invoke(other.gameObject);
            Done = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CheckForTagMatch(other.gameObject) && (!_doOnce || !Done))
        {
            OnExit.Invoke(other.gameObject);
            Done = true;
        }
    }

    private bool CheckForTagMatch(GameObject other)
    {
        // ignore tag matching if tag is empty
        if (string.IsNullOrEmpty(other.tag)) return true;
        // compare tags
        return other.CompareTag(_tagFilter);
    }

#if UNITY_EDITOR
    [MenuItem("GameObject/Volume/Trigger Volume2D", false, 0)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("Trigger Volume");
        go.AddComponent<TriggerVolume2D>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
#endif
}