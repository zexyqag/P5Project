using System;
using UnityEngine;
using UnityEngine.Events;

public class StringMatchChecker : MonoBehaviour {

	#region Public var
	public UnityEvent onStringMatch, onCharacterError;
	#endregion

	#region Private var
	private string stringToMatch = "";
	private int currentStringElement = 0, lastCorrectElement = 0;
	private bool isAnyLetterIncorrect = false;
	#endregion


	private void Awake() {
		EventSystem.onButtonPressed += checkCharacterForMatch;
		EventSystem.onValidateSentence += checkStringForMatch;
		EventSystem.onSwtichInputMethod += resetProgress;
		EventSystem.onUpdateStringToMatch += updateStringToMatch;
		EventSystem.onBackspace += backspace;
		EventSystem.onResetStringMatchChecker += resetProgress;
	}

	private void resetProgress() {
		currentStringElement = 0;
		lastCorrectElement = 0;
		isAnyLetterIncorrect = false;
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
				EventSystem.onTypedCorrect(c);

				if(isAnyLetterIncorrect == false) {
					lastCorrectElement = currentStringElement;
					EventSystem.onChangeColorCorrect();
				}

			} else {
				onCharacterError.Invoke();
				EventSystem.onTypedError(c);
				isAnyLetterIncorrect = true;
			}
		}
		++currentStringElement;
	}



	void backspace() {
		if(currentStringElement > 0) {
			--currentStringElement;

			if(lastCorrectElement > currentStringElement) {
				lastCorrectElement = currentStringElement - 1;
			}

			if(currentStringElement == lastCorrectElement + 1) {
				isAnyLetterIncorrect = false;
				EventSystem.onChangeColorCorrect();
			}
		}
	}
}
