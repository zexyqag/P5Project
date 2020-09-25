using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBoard : MonoBehaviour {
	private Text textFlied;
	private void Start() {
		EventSystem.onButtonPressed += AddText;
		EventSystem.clearKeyboard += deleteKeyboardText;
		textFlied = this.GetComponent<UnityEngine.UI.Text>();
		FlashIndicator();

	}

	void AddText(char letterToAdd) {
		if(letterToAdd.Equals('<')) {
			DeleteText();
			return;
		}

		textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1);
		textFlied.text += letterToAdd;
		EventSystem.validateScentence(textFlied.text);
		textFlied.text += "|";
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
