using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
	private AudioSource audioSource;
	public SoundManagerScriptableObject soundManager;
	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}


}
