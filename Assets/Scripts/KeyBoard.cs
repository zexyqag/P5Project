using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;

public class KeyBoard : MonoBehaviour {
	private Text textFlied;
	private void Start() {
		EventSystem.onButtonPressed += AddText;
		EventSystem.onClearKeyboard += deleteKeyboardText;
		EventSystem.onBackspace += Backspace;
		textFlied = this.GetComponent<UnityEngine.UI.Text>();
		FlashIndicator();

	}

	void AddText(char letterToAdd) {
		if(letterToAdd.Equals('<')) {
			DeleteText();
			return;
		}

		textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1);
		Debug.Log("Check addletter");
		textFlied.text += letterToAdd;
		EventSystem.onValidateScentence(textFlied.text);
		textFlied.text += "|";
	}

	void Backspace() {
		if(textFlied.text.Length >= 2) {
			textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 2) + "|";
		}
	}

	void DeleteText() {
		if(textFlied.text.Length <= 1) { return; }

		textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1);

	}

	void FlashIndicator() {
		StartCoroutine(WaitforTime(0.5f));
	}

	private void deleteKeyboardText() {
		textFlied.text = "";
	}

	IEnumerator WaitforTime(float timeToWait) {
		if(textFlied.text.Length >= 1) {
			yield return new WaitForSeconds(timeToWait);
			textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1) + " ";

			yield return new WaitForSeconds(timeToWait);
			textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1) + "|";
		}

		FlashIndicator();
	}
}
