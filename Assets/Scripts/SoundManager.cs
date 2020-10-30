using System;
using UnityEngine;
[Serializable] public enum enAudio { Correct, Wrong, KeyboardClick, Miss };
public class SoundManager : MonoBehaviour {

	#region Private Var
	[SerializeField] private AudioClip Correct = null, Wrong = null, KeyboardClick = null, Miss = null, MissingAudio = null;
	#endregion

	#region Public Var
	public static SoundManager Instance { get; private set; }
	#endregion

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(this);
		}
	}

	public AudioClip getAudio(enAudio audioToPlay) {
		if(audioToPlay == enAudio.Correct) {
			return Correct;
		}else if(audioToPlay == enAudio.KeyboardClick) {
			return KeyboardClick;
		}else if(audioToPlay == enAudio.Wrong) {
			return Wrong;
		} else if(audioToPlay == enAudio.Miss) {
			return Miss;
		} else {
			return MissingAudio;
		}
	}
}