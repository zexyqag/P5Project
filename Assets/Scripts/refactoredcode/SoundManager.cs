using System;
using UnityEngine;
[Serializable] public enum enAudio { Correct, Wrong, KeyboardClick, Miss, MissingAudio };
public class SoundManager : MonoBehaviour {

	#region Private Var
	[SerializeField] private AudioClip Correct = null, Wrong = null, KeyboardClick = null, Miss = null, MissingAudio = null;
	#endregion

	#region Public Var
	public static SoundManager Instance { get; private set; }
	#endregion

	private void Awake() {
		if (Instance == null) { //Singelton pattern
			Instance = this;
		} else {
			Destroy(this);
		}
	}
	

	/// <summary>
	/// Gets the audio asiggend to the corosponedening SoundManager singelton
	/// </summary>
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