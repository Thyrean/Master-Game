using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalInputGroup : InputGroup
{
    public event Action<int[]> numbersChanged;

    private int[] numbers;

    protected override void Awake()
    {
        base.Awake();

        numbers = new int[elements.Length];
    }

    protected override void SetText(string text)
    {
        if (!int.TryParse(text, out int number))
            return;

        numbers[currentFocusedIndex] = number;
        elements[currentFocusedIndex].SetText(text);
        numbersChanged?.Invoke(numbers);
    }
}
