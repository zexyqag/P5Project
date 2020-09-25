using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StringsToType : MonoBehaviour {
	public StringArrayScriptableObject StringArray;
	private List<String> strings = new List<string>();
	private Text textField;

	public UnityEvent StringMatch;

	private void Awake() {
		textField = GetComponent<Text>();
	}

	private void Start() {
		strings = StringArray.getStringsToType();
		strings.OrderBy((item) => new System.Random().Next());
		nextText();
	}


	public void CheckStringForMatch(String input) {
		if(input.Equals(textField.text)) {
			nextText();
		}
	}

	private void nextText() {
		if(strings.Count != 0) {
			textField.text = strings.FirstOrDefault<String>();
			strings.RemoveAt(0);
		} else {
			Debug.Log("No more strings :c");
		}
	}
}
