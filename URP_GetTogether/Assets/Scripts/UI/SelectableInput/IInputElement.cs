using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputElement
{
    void Focus(Color focusColor);
    void UnFocus();
    void SetText(string text);
    void AppendText(string text);

    event Action<string> onValueChanged;
}
