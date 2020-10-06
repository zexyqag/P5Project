using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StringMatchChecker : MonoBehaviour {
	public UnityEvent onStringMatch, onCharacterError;
	private string stringToMatch = "";
	private int currentStringElement = 0;
	[SerializeField] private bool isRaycastScene;

	private void Awake() {
		EventSystem.onButtonPressed += checkCharacterForMatch;
		EventSystem.onValidateSentence += checkStringForMatch;
		EventSystem.onUpdateStringToMatch += updateStringToMatch;
	}

	private void updateStringToMatch(string s) {
		stringToMatch = s;
	}

	public void checkStringForMatch(string s) {
		if(stringToMatch.Equals(s)) {
			EventSystem.onNextString();
			onStringMatch.Invoke();
		}
	}

	public void checkCharacterForMatch(char c) {
		if(stringToMatch.Length < currentStringElement) {
			if(stringToMatch[currentStringElement].Equals(c)) {
				++currentStringElement;
				EventSystem.onTypedCorrect(isRaycastScene);
			} else {
				onCharacterError.Invoke();
				EventSystem.onTypedError(isRaycastScene);
			}
		}
	}
}
