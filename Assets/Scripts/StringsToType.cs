using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StringsToType : MonoBehaviour {
	public StringArrayScriptableObject StringArray;
	private string[] strings;
	private Text textField;

	private void Start() {
		textField.GetComponent<Text>();
		strings = StringArray.StringsToType;

	}


	public void CheckStringForMatch() {

	}
}
