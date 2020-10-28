using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTest : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("UpAndDown")) {
            StartTesting();
        }
    }

    [ContextMenu("Start Testing")]
	private void StartTesting() {
        SceneManager.LoadScene(1);
    }
}
