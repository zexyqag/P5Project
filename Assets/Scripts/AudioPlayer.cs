using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
	private AudioSource audioSource;
	[SerializeField] private enAudio audioToPlay;
	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}
	private void Start() {
		audioSource.clip = SoundManager.Instance.getAudio(audioToPlay);
	}

	[ContextMenu("playAudio")]
	public void playAudio() {
		if(audioSource.isPlaying) {
			audioSource.Stop();
		}
		audioSource.Play();
	}
}
