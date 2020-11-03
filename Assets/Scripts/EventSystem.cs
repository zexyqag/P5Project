using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public static class EventSystem {
	public static Action<char> onButtonPressed, onTypedError, onTypedCorrect;
	public static Action<string> onValidateSentence, onUpdateStringToMatch, onTestType;
	public static Action onClearKeyboard, onBackspace, onNextString, onSaveData, onSetPos, onChangeColorCorrect, onSwtichInputMethod, onResetStringMatchChecker, onStartTest;
}
