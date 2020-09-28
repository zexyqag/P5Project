using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Text))]
public class StringsToType : MonoBehaviour {
	public StringArrayScriptableObject StringArray;
	private List<string> strings = new List<string>();
	private Text textField;
	private int elementTracker;
	public TextAsset phrasesToWrite;

	//public UnityEvent OnStringMatch;

	private void Awake() {
		textField = GetComponent<Text>();
		elementTracker = 0;
		if(phrasesToWrite) {
			strings = new List<string>(phrasesToWrite.text.Split('\n'));
		} else {
			Debug.Log("Not text file assigned to: " + this + " on " + gameObject.name);
		}
	}

	private void Start() {
		Initialize();
		EventSystem.onValidateScentence += CheckStringForMatch;
		EventSystem.onButtonPressed += doesCharacterMatch;
		EventSystem.onBackspace += Backspace;
	}

	private void Initialize() {
		strings = StringArray.getStringsToTypeCopy();
		strings.Shuffle();
		nextText();
	}

	private void doesCharacterMatch(char inputLetter) {
		if(textField.text.Length > elementTracker) {
			if(inputLetter != textField.text[elementTracker]) {
				Debug.Log("!Correct");
			}
		}

		elementTracker++;
	}

	private void Backspace() {
		if(elementTracker == 0)
			return;

		elementTracker--;
	}

	public void CheckStringForMatch(string input) {
		if(input.ToLower().Equals(textField.text.ToLower())) {
			//OnStringMatch.Invoke();
			EventSystem.onClearKeyboard();
			elementTracker = 0;
			nextText();
		}
	}

	[ContextMenu("nextText")]
	private void nextText() {
		if(strings.Count != 0) {
			textField.text = strings.First<string>();
			strings.RemoveAt(0);
		} else {
			Initialize();
		}
	}
}
