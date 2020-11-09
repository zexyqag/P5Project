using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBoard : MonoBehaviour {

	private Text textField;
	public Color corretColer = Color.green, wrongColor = Color.red;

	private void Awake() {
		EventSystem.onButtonPressed += AddText;
		EventSystem.onClearKeyboard += deleteKeyboardText;
		EventSystem.onChangeColorCorrect += changeCorrectMaterial;
		EventSystem.onTypedError += changeWrongMaterial;
		EventSystem.onBackspace += Backspace;
		EventSystem.onSwtichInputMethod += resetTextField;
	}

	private void Start() {
		textField = this.GetComponent<Text>();
		FlashIndicator();
	}

	void AddText(char letterToAdd) {

		textField.text = textField.text.Substring(0, textField.text.Length - 1);
		textField.text += letterToAdd;
		EventSystem.onValidateSentence(textField.text);
		textField.text += "|";
	}

	void changeCorrectMaterial() {
		textField.color = corretColer;
	}

	void changeWrongMaterial(char _) {
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

	private void resetTextField() {
		textField.text = "|";
	}

	IEnumerator WaitforTime(float timeToWait) {
		yield return new WaitForSeconds(timeToWait);
		blink(" ");

		yield return new WaitForSeconds(timeToWait);
		blink("|");
		FlashIndicator();
	}

	private void blink(string s) {
		textField.text = textField.text.Length >= 1 ? textField.text.Substring(0, textField.text.Length - 1) + s : "|";
	}

	private void OnApplicationQuit() => Unsubscribe();
	private void OnDisable() => Unsubscribe();
	private void OnDestroy() => Unsubscribe();
	private void Unsubscribe() {
		EventSystem.onButtonPressed -= AddText;
		EventSystem.onClearKeyboard -= deleteKeyboardText;
		EventSystem.onChangeColorCorrect -= changeCorrectMaterial;
		EventSystem.onTypedError -= changeWrongMaterial;
		EventSystem.onBackspace -= Backspace;
		EventSystem.onSwtichInputMethod -= resetTextField;
	}
}
