using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBoard : MonoBehaviour 
{

	#region Public Fields
	public Color corretColer = Color.green, wrongColor = Color.red;
	#endregion

	#region Private Fields
	private Text textField;
	#endregion

	#region Unity Functions
	private void Awake()
	{// Awake happens before start, this functions subscripe to all relevant events

		EventSystem.onButtonPressed += AddText;
		EventSystem.onClearKeyboard += deleteKeyboardText;
		EventSystem.onChangeColorCorrect += ChangeToCorrectMaterial;
		EventSystem.onTypedError += ChangeToWrongMaterial;
		EventSystem.onBackspace += Backspace;
		EventSystem.onSwtichInputMethod += resetTextField;
	}

	private void Start()
	{
		textField = this.GetComponent<Text>();	// Defines textField as the Text of this
		FlashIndicator();
	}
	#endregion

	#region Change Materials of textfield

	void ChangeToCorrectMaterial()
	{
		textField.color = corretColer;
	}

	void ChangeToWrongMaterial(char _)
	{ // Char as descrete perameter becouse of the event, but it is not used in this function, therefor "_"
		textField.color = wrongColor;
	}
	#endregion

	#region Add and Remove text

	void AddText(char letterToAdd)
	{
		textField.text = textField.text.Substring(0, textField.text.Length - 1);    // Substring is used to devide the string in the textfield from the start til the end minus 1
		textField.text += letterToAdd;                                              // Adds the newly typed letter to the text field string
		EventSystem.onValidateSentence(textField.text);                             // Invokes the event to validate the sentence
		textField.text += "|";
	}

	void Backspace()
	{
		if (textField.text.Length >= 2)
		{                                                   // Only if the length of the textfield is 2 or more, should the backspace delete
			textField.text = textField.text.Substring(0, textField.text.Length - 2) + "|";  // Delete the last 2 elements, one is the letter to delete, the other is the blinking indicator
		}
	}

	private void deleteKeyboardText()
	{
		textField.text = ""; // Sets the intire text field string to empty
	}

	private void resetTextField()
	{
		textField.text = "|"; // Adds the "|" needed for the Flash indicator
	}
	#endregion

	#region Blincking FlashIndicator
	void FlashIndicator()
	{
		StartCoroutine(WaitforTime(0.5f)); // starts a countdown
	}

	IEnumerator WaitforTime(float timeToWait) 
	{
		yield return new WaitForSeconds(timeToWait);
		blink(" ");

		yield return new WaitForSeconds(timeToWait);
		blink("|");
		FlashIndicator();
	}

	private void blink(string s)
	{
		textField.text = textField.text.Length >= 1 ? textField.text.Substring(0, textField.text.Length - 1) + s : "|";
	}

	#endregion

	#region Unsubscribe
	private void OnApplicationQuit() => Unsubscribe();
	private void OnDisable() => Unsubscribe();
	private void OnDestroy() => Unsubscribe();
	private void Unsubscribe()
	{
		EventSystem.onButtonPressed -= AddText;
		EventSystem.onClearKeyboard -= deleteKeyboardText;
		EventSystem.onChangeColorCorrect -= ChangeToCorrectMaterial;
		EventSystem.onTypedError -= ChangeToWrongMaterial;
		EventSystem.onBackspace -= Backspace;
		EventSystem.onSwtichInputMethod -= resetTextField;
	}
	#endregion
}
