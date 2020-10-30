using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class StartTest : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("UpAndDown")) {
            StartTesting();
        }
    }

    [ContextMenu("Start Testing")]
	private void StartTesting() {
        Destroy(Player.instance.gameObject);
        SceneManager.LoadScene(1);
    }
}
