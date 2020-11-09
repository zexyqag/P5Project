using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {

	#region Private fields
	private AudioSource audioSource;
	[SerializeReference] private enAudio audioToPlay = enAudio.MissingAudio;
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
	/// Play the audio assigend
	/// </summary>
	public void playAudio() {
		if(audioSource.isPlaying) {
			audioSource.Stop();
		}
		audioSource.PlayOneShot(SoundManager.Instance.getAudio(audioToPlay));
	}

	/// <summary>
	/// Play audio corresponding to SoundManager based on enAduio Enum int
	/// </summary>
	public void playAudio(int audioToPlay) {
		if(audioSource.isPlaying) {
			audioSource.Stop();
		}
		audioSource.PlayOneShot(SoundManager.Instance.getAudio((enAudio)audioToPlay));
	}
}
