using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject _move;
    [SerializeField] private GameObject _spit;
    [SerializeField] private GameObject _fly;
    [SerializeField] private GameObject _tourist;

    private bool _moveCanEnter = true;
    private bool _spitCanEnter = true;
    private bool _flyCanEnter = true;
    private bool _touristCanEnter = true;

    public void Start()
    {
        StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        _move.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.LeftShift))
            {
                _move.SetActive(false);
                break;
            }

            yield return null;
        }
    }

    private IEnumerator Spit()
    {
        _spit.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _spit.SetActive(false);
                break;
            }

            yield return null;
        }
    }

    private IEnumerator Fly()
    {
        _fly.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fly.SetActive(false);
                break;
            }

            yield return null;
        }
    }

    private IEnumerator Tourist()
    {
        _tourist.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _tourist.SetActive(false);
                break;
            }

            yield return null;
        }
    }

    public void StartSpit()
    {
        if (_spitCanEnter)
            StartCoroutine(Spit());

        _spitCanEnter = false;
    }
    public void StartFly()
    {
        if (_flyCanEnter)
            StartCoroutine(Fly());

        _flyCanEnter = false;
    }
    public void StartTourist()
    {
        if (_touristCanEnter)
            StartCoroutine(Tourist());

        _touristCanEnter = false;
    }
}
