using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ActionReactionSystem;


public class InputGroupNumberChecker : MonoBehaviour
{
    [SerializeField] private NumericalInputGroup _inputGroup;
    [SerializeField] private int[] _correctNumbers;


    private void Awake()
    {
        _inputGroup.numbersChanged += UpdateNumbers;
        
        if(_correctNumbers.Length != _inputGroup.ElementCount)
        {
            Debug.LogWarning("WARNING ! Number of correct inputs does not match the input fields !");
        }
    }

    private void UpdateNumbers(int[] numbers)
    {
        for(var i=0; i < _correctNumbers.Length; i++)
        {
            var match = numbers[i] == _correctNumbers[i];
            if (!match) return;
        }

        OnCorrectEntered();
    }

    private void OnCorrectEntered()
    {
        Debug.Log("YAY!");

        ReactionManager.Call("OpenCodeDoor");
    }
}
