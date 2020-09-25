using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StringArrayScriptableObject", order = 1)]
public class StringArrayScriptableObject : ScriptableObject {
	public string prefabName;
	public string[] StringsToType;
}
