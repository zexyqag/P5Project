using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Text))]
public class StringsToType : MonoBehaviour {
	public StringArrayScriptableObject StringArray;
	private List<string> strings = new List<string>();
	private Text textField;

	public UnityEvent OnStringMatch;

	private void Awake() {
		textField = GetComponent<Text>();
	}

	private void Start() {
		Initialize();
		EventSystem.validateScentence += CheckStringForMatch;
	}

	private void Initialize() {
		strings = StringArray.getStringsToTypeCopy();
		strings.Shuffle();
		nextText();
	}

	public void CheckStringForMatch(string input) {
		if(input.Equals(textField.text)) {
			OnStringMatch.Invoke();
			EventSystem.clearKeyboard();
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
