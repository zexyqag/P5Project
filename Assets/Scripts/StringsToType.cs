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
	private List<String> strings = new List<string>();
	private Text textField;

	public UnityEvent OnStringMatch;

	private void Awake() {
		textField = GetComponent<Text>();
	}

	private void Start() {
		Initialize();
	}

	private void Initialize() {
		strings = StringArray.getStringsToTypeCopy();
		strings.Shuffle();
		nextText();
	}

	public void CheckStringForMatch(String input) {
		if(input.Equals(textField.text)) {
			OnStringMatch.Invoke();
			nextText();
		}
	}

	[ContextMenu("nextText")]
	private void nextText() {
		if(strings.Count != 0) {
			textField.text = strings.First<String>();
			strings.RemoveAt(0);
		} else {
			Initialize();
		}
	}
}
