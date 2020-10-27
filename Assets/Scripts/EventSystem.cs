using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public static class EventSystem {
	public static Action<char> onButtonPressed;
	public static Action<string> onValidateSentence, onUpdateStringToMatch;
	public static Action onClearKeyboard, onBackspace, onNextString, onSaveData, onSetPos, onChangeColorCorrect, onTypedError, onTypedCorrect;
}
