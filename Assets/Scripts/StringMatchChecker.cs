﻿using UnityEngine;
using UnityEngine.Events;

public class StringMatchChecker : MonoBehaviour {

	#region Public fields
	public UnityEvent onStringMatch, onCharacterError;
	#endregion

	#region Private fields
	private string stringToMatch = string.Empty, stringProgress = string.Empty;
	private int currentStringElement = 0, lastCorrectElement = 0;
	private bool isAnyLetterIncorrect = false;
	private string InputTextFieldString;
	#endregion

	#region Unity fields
	private void Awake() {
		EventSystem.onValidateCharacter += checkCharacterForMatch;
		EventSystem.onValidateSentence += checkStringForMatch;
		EventSystem.onSwtichInputMethod += resetProgress;
		EventSystem.onUpdateStringToMatch += updateStringToMatch;
		EventSystem.onBackspace += backspace;
		EventSystem.onResetStringMatchChecker += resetProgress;
	}
	#endregion

	/// <summary>
	/// Resets the progress of the StringMatchChecker making it ready for a new string.
	/// </summary>
	private void resetProgress() {
		currentStringElement = 0;
		lastCorrectElement = 0;
		isAnyLetterIncorrect = false;
	}	

	/// <summary>
	/// Sets the string to match to the string provided as parameter.
	/// </summary>
	private void updateStringToMatch(string s) {
		stringToMatch = s.ToUpper();
	}

	/// <summary>
	/// Checks whether or not the stringToMatch string matches with the string provided as a parameter.
	/// </summary>
	public void checkStringForMatch(string s) {
		if(stringToMatch.Equals(s)) {
			EventSystem.onNextString();
			EventSystem.onClearKeyboard();
			onStringMatch.Invoke();
			resetProgress();
		}
	}

	/// <summary>
	/// Checks whether or not the current character matches with the character provided as a parameter
	/// </summary>
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

	/// <summary>
	/// Updates the currentStringElemnt to match when pressing backspace
	/// </summary>
	void backspace() {
		if(currentStringElement > 0) {
			--currentStringElement;

			if(lastCorrectElement >= currentStringElement) {
				lastCorrectElement = currentStringElement - 1;
			}

			if(currentStringElement == lastCorrectElement + 1) {
				isAnyLetterIncorrect = false;
				EventSystem.onChangeColorCorrect();
			}
		}
	}

	#region Unsubscrips
	/// <summary>
	/// Unsubscribes the methods in this script from the EventSystem
	/// </summary>
	private void Unsubscribe() {
		EventSystem.onButtonPressed -= checkCharacterForMatch;
		EventSystem.onValidateSentence -= checkStringForMatch;
		EventSystem.onSwtichInputMethod -= resetProgress;
		EventSystem.onUpdateStringToMatch -= updateStringToMatch;
		EventSystem.onBackspace -= backspace;
		EventSystem.onResetStringMatchChecker -= resetProgress;
	}

	private void OnApplicationQuit() => Unsubscribe();
	private void OnDisable() => Unsubscribe();
	private void OnDestroy() => Unsubscribe();
	#endregion

}
