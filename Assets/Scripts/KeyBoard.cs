using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBoard : MonoBehaviour {
	private Text textField;
	private void Start() {
		EventSystem.onButtonPressed += AddText;
		EventSystem.onClearKeyboard += deleteKeyboardText;
		EventSystem.onBackspace += Backspace;
		textField = this.GetComponent<UnityEngine.UI.Text>();
		FlashIndicator();

	}

	void AddText(char letterToAdd) {

		textField.text = textField.text.Substring(0, textField.text.Length - 1);
		Debug.Log("Check addletter");
		textField.text += letterToAdd;
		EventSystem.onValidateSentence(textField.text);
		textField.text += "|";
	}

	void Backspace() {
		if(textField.text.Length >= 2) {
			textField.text = textField.text.Substring(0, textField.text.Length - 2) + "|";
		}
	}

	void FlashIndicator() {
		StartCoroutine(WaitforTime(0.5f));
	}

	private void deleteKeyboardText() {
		textField.text = "|";
	}

	IEnumerator WaitforTime(float timeToWait) {
		if(textField.text.Length >= 1) {
			yield return new WaitForSeconds(timeToWait);
			textField.text = textField.text.Substring(0, textField.text.Length - 1) + " ";

			yield return new WaitForSeconds(timeToWait);
			textField.text = textField.text.Substring(0, textField.text.Length - 1) + "|";
		}

		FlashIndicator();
	}
}
