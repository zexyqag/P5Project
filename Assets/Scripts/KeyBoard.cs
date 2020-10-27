using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBoard : MonoBehaviour {
	private Text textField;

    public Color corretColer = Color.green, wrongColor = Color.red;

	private void Start() {
		EventSystem.onButtonPressed += AddText;
		EventSystem.onClearKeyboard += deleteKeyboardText;
        EventSystem.onChangeColorCorrect += changeCorrectMaterial;
        EventSystem.onTypedError += changeWrongMaterial;
        EventSystem.onBackspace += Backspace;
		EventSystem.onSwtichInputMethod += resetTextField;
		textField = this.GetComponent<UnityEngine.UI.Text>();
		FlashIndicator();

	}

	void AddText(char letterToAdd) {

		textField.text = textField.text.Substring(0, textField.text.Length - 1);
		textField.text += letterToAdd;
		EventSystem.onValidateSentence(textField.text);
		textField.text += "|";
	}

    void changeCorrectMaterial()
    {
        textField.color = corretColer;
    }

    void changeWrongMaterial()
    {
        textField.color = wrongColor;
    }

    [ContextMenu("Backspace")]
	void Backspace() {
		if(textField.text.Length >= 2) {
			textField.text = textField.text.Substring(0, textField.text.Length - 2) + "|";
		}
	}

	void FlashIndicator() {
		StartCoroutine(WaitforTime(0.5f));
	}

	private void deleteKeyboardText() {
		textField.text = "";
	}

	private void resetTextField()
	{
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
