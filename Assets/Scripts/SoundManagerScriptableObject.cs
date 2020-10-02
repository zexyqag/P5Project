using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundManagerScriptableObject", order = 1)]
public class SoundManagerScriptableObject : ScriptableObject {
	public AudioClip Correct, Wrong, KeyboardClick;
}
