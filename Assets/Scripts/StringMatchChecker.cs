using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StringMatchChecker : MonoBehaviour {
	public UnityEvent onStringMatch, onCharacterError;
	private string stringToMatch = "";
	private int currentStringElement = 0;
    private int lastCorrectElement;
	[SerializeField] private bool isRaycastScene = false;

	private void Awake() {
		EventSystem.onButtonPressed += checkCharacterForMatch;
		EventSystem.onValidateSentence += checkStringForMatch;
		EventSystem.onUpdateStringToMatch += updateStringToMatch;
		EventSystem.onBackspace += backspace;
	}

	private void updateStringToMatch(string s) {
		stringToMatch = s.ToUpper();
	}

	public void checkStringForMatch(string s) {
		if(stringToMatch.Equals(s)) {
			EventSystem.onClearKeyboard();
			EventSystem.onNextString();
			currentStringElement = 0;
			onStringMatch.Invoke();
		}
	}

	public void checkCharacterForMatch(char c) {
		if(stringToMatch.Length > currentStringElement) {
			if(stringToMatch[currentStringElement].Equals(c)) {
                lastCorrectElement = currentStringElement;
				EventSystem.onTypedCorrect(isRaycastScene);
                EventSystem.onChangeColorCorrect();
            } else {
				onCharacterError.Invoke();
				EventSystem.onTypedError(isRaycastScene);
			}
		}
		++currentStringElement;
	}

	

	void backspace() {
		if(currentStringElement > 0) {
			--currentStringElement;

            if (currentStringElement == lastCorrectElement +1)
            {
                EventSystem.onChangeColorCorrect();
            }
        }
	}

	[ContextMenu("invokeOnStringMatch")]
	public void invokeOnStringMatch() {
		onStringMatch.Invoke();
	}

	[ContextMenu("invokeonCharacterError")]
	public void invokeonCharacterError() {
		onCharacterError.Invoke();
	}
}
