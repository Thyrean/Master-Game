using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkedInputElement : MonoBehaviour, IInputElement
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _backGround;

    public event Action<string> onValueChanged;
    public event Action<NetworkedInputElement> OnChanged;

    private bool _focused;
    private Color defaultColor;

    public Color Color
    {
        get => _backGround.color;
        set
        {
            _backGround.color = value;
            _text.color = value;
        }
    }

    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    private void Awake()
    {
        defaultColor = _backGround.color;
    }

    public void SetText(string text)
    {
        if (!_focused) return;

        Text = text;
        OnChanged?.Invoke(this);
    }
    public void AppendText(string text)
    {
        if (!_focused) return;

        Text = Text + text;
    }

    public void Focus(Color color)
    {
        if (_focused) return;

        _focused = true;
        Color = color;
        OnChanged?.Invoke(this);
    }
    public void Focus()
    {
        if (_focused) return;

        _focused = true;
        Color = Color.red;
        OnChanged?.Invoke(this);
    }

    public void UnFocus()
    {
        if (!_focused) return;

        OnChanged?.Invoke(this);
        Color = defaultColor;
        _focused = false;
    }
}

