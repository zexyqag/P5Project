using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {

	#region Private fields
	private AudioSource audioSource = null; //The audio clip to play
	[SerializeReference] private enAudio audioToPlay = enAudio.MissingAudio; //This enum dictates which sound to play, defaults to MissingAudio if none is specified in the inspector
	#endregion

	#region Unity fields
	//Save references to dependent components
	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	//Get audio clip to play from sound manager singelton
	private void Start() {
		audioSource.clip = SoundManager.Instance.getAudio(audioToPlay);
	}
	#endregion

	/// <summary>
	/// Play the assigend audio clip
	/// </summary>
	public void playAudio() {
		if(audioSource.isPlaying) {
			audioSource.Stop();
		}
		audioSource.PlayOneShot(SoundManager.Instance.getAudio(audioToPlay));
	}

	/// <summary>
	/// Play an audio clip corresponding to SoundManager based on enAduio Enum int
	/// </summary>
	public void playAudio(int audioToPlay) {
		if(audioSource.isPlaying) {
			audioSource.Stop();
		}
		audioSource.PlayOneShot(SoundManager.Instance.getAudio((enAudio)audioToPlay));
	}
}
