using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputGroup : MonoBehaviour
{
    protected IInputElement[] elements;
    protected int currentFocusedIndex = 0;

    [SerializeField] private KeyCode[] nextKeys;
    [SerializeField] private KeyCode[] prevKeys;
    [SerializeField] private Color focusColor;

    private bool _focused;
    public bool Focused
    {
        get => _focused;
        set
        {
            if (_focused == value) return;
            _focused = value;
            //currentFocusedIndex = 0;
            if (value)
                elements[currentFocusedIndex].Focus(focusColor);
            else
                elements[currentFocusedIndex].UnFocus();
        }
    }

    public int ElementCount => elements.Length;

    protected virtual void Awake()
    {
        elements = transform.GetComponentsInChildren<IInputElement>();
    }

    private void Update()
    {
        if (!Focused) return;
        CheckInput();
    }

    private void CheckInput()
    {
        foreach(var key in nextKeys) 
            if (Input.GetKeyUp(key)) IncrementFocus();
        foreach (var key in prevKeys)
            if (Input.GetKeyUp(key)) DecrementFocus();
        
        if (Input.inputString != "")
            SetText(Input.inputString);
    }

    protected abstract void SetText(string text);


    private void IncrementFocus()
    {
        elements[currentFocusedIndex].UnFocus();
        currentFocusedIndex++;
        if (currentFocusedIndex >= elements.Length) currentFocusedIndex = 0;
        elements[currentFocusedIndex].Focus(focusColor);
    }

    private void DecrementFocus()
    {
        elements[currentFocusedIndex].UnFocus();
        currentFocusedIndex--;
        if (currentFocusedIndex < 0) currentFocusedIndex = elements.Length - 1;
        elements[currentFocusedIndex].Focus(focusColor);
    }


}
