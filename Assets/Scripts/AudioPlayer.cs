using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
	private AudioSource audioSource;
	[SerializeReference] private enAudio audioToPlay;
	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}
	private void Start() {
		/*if (audioToPlay) {
			Debug.LogWarning(this.ToString() + "No audio selected");
		}*/
		audioSource.clip = SoundManager.Instance.getAudio(audioToPlay);
	}

	[ContextMenu("playAudio")]
	public void playAudio() {
		if(audioSource.isPlaying) {
			audioSource.Stop();
		}
		audioSource.PlayOneShot(SoundManager.Instance.getAudio(audioToPlay));
	}

	public void playAudio(int audioToPlay) {
		if(audioSource.isPlaying) {
			audioSource.Stop();
		}
		audioSource.PlayOneShot(SoundManager.Instance.getAudio((enAudio)audioToPlay));
	}
}
