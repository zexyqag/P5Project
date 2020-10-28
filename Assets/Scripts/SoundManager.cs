using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	[SerializeField] private AudioClip Correct = null, Wrong = null, KeyboardClick = null, Miss = null, MissingAudio = null;
	public static SoundManager Instance { get; private set; }

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
[Serializable] public enum enAudio { Correct, Wrong, KeyboardClick, Miss };