using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventSystem
{
    public static Action<char> onButtonPressed;
    public static Action<string> onValidateScentence;
    public static Action onClearKeyboard;
    public static Action onBackspace;
}
