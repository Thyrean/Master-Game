using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputElement : MonoBehaviour, IInputElement
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _backGround;

    public event Action<string> onValueChanged;

    private bool _focused;
    private Color defaultColor;
    private Color defaultFontColor;

    private void Awake()
    {
        defaultColor = _backGround.color;
        defaultFontColor = _text.color;
    }

    public void SetText(string text)
    {
        if (!_focused) return;

        _text.text = text;
        onValueChanged?.Invoke(text);
    }
    public void AppendText(string text)
    {
        if (!_focused) return;

        _text.text += text;
        onValueChanged?.Invoke(_text.text);
    }

    public void Focus(Color color)
    {
        _focused = true;
        _backGround.color = color;
        _text.color = color;
    }
    public void Focus()
    {
        _focused = true;
        _backGround.color = Color.red;
        _text.color = Color.red;
    }

    public void UnFocus()
    {
        _backGround.color = defaultColor;
        _text.color = defaultFontColor;
        _focused = false;
    }
}
