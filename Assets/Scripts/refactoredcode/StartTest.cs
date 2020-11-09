using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class StartTest : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if(other.CompareTag("UpAndDown"))
			StartTesting();
	}

	/// <summary>
	/// Loads the testScene and destorys the current player instance.
	/// </summary>
	[ContextMenu("Start Testing")]
	private void StartTesting() {
		Destroy(Player.instance.gameObject);
		SceneManager.LoadScene(1);
	}
}
