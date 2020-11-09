using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class ChangeScene : MonoBehaviour {
	public void LoadScene(int i) {
		if(Player.instance)
			Destroy(Player.instance.gameObject);
		SceneManager.LoadScene(i);
	}
}
