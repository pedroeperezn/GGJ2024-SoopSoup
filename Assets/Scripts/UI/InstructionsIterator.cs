using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsIterator : MonoBehaviour
{
    [SerializeField] private GameObject[] _instructionPages;
    private static int _currIndex = 0;
    public void LeftIterate()
    {
        _instructionPages[_currIndex].SetActive(false);
        _currIndex = (_currIndex - 1) % _instructionPages.Length; 

        if(_currIndex < 0 )
            _currIndex = _instructionPages.Length - 1;

        _instructionPages[_currIndex].SetActive(true);
    }

    public void RightIterate()
    {
        _instructionPages[_currIndex].SetActive(false);
        _currIndex = (_currIndex + 1) % _instructionPages.Length;
        _instructionPages[_currIndex].SetActive(true);
    }  
}
