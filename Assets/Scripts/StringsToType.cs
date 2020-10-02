using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Text))]
public class StringsToType : MonoBehaviour {
	private List<string> phrasesToWriteStringList = new List<string>();
	private Text textField;
	private int elementTracker = 0, phraseIndex = 0, errorRate = 0;
	public TextAsset phrasesToWriteAsset, errorRateAsset;
	public bool isRaycastSecene;

	private void Awake() {
		textField = GetComponent<Text>();

		if(phrasesToWriteAsset) {
			using(System.IO.StringReader reader = new System.IO.StringReader(phrasesToWriteAsset.text)) {
				string line;
				while((line = reader.ReadLine()) != null) {
					phrasesToWriteStringList.Add(line);
				}
			}
		} else {
			Debug.Log("Not text file assigned to: " + this + " on " + gameObject.name);
			phrasesToWriteStringList = new List<string> { "No text file assigned", "You forgot to assgin a text file", "Missing text file", "Text file be gone" };
		}
	}

	private void Start() {
		Initialize();
		EventSystem.onValidateScentence += CheckStringForMatch;
		EventSystem.onButtonPressed += doesCharacterMatch;
		EventSystem.onBackspace += Backspace;
	}

	[ContextMenu("shuffle list")]
	private void Initialize() {
		phraseIndex = 0;
		phrasesToWriteStringList.Shuffle();
		nextText();
	}

	private void doesCharacterMatch(char inputLetter) {
		if(textField.text.Length > elementTracker) {
			if(inputLetter != textField.text[elementTracker]) {
				EventSystem.onTypedError(isRaycastSecene);
			} else {
				EventSystem.onTypedCorrect(isRaycastSecene);
			}
		}
		elementTracker++;
	}

	private void Backspace() {
		if(elementTracker == 0)
			return;

		--elementTracker;
	}

	public void CheckStringForMatch(string input) {
		if(input.Equals(textField.text)) {
			EventSystem.onClearKeyboard();
			elementTracker = 0;
			nextText();
		}
	}

	[ContextMenu("nextText")]
	private void nextText() {
		if(phraseIndex >= phrasesToWriteStringList.Count) {
			Initialize();
		} else {
			textField.text = phrasesToWriteStringList.ElementAt<string>(phraseIndex).ToUpper();
			++phraseIndex;
		}
	}
}
