using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StringArrayScriptableObject", order = 1)]
public class StringArrayScriptableObject : ScriptableObject {
	[SerializeField] private List<String> StringsToType = new List<string>();
	[SerializeField] private TextAsset phrasesToWrite;
	private void Awake() {
		if(phrasesToWrite) {

		}
	}
	public List<String> getStringsToTypeCopy() {
		return new List<string>(StringsToType);
	}
}
