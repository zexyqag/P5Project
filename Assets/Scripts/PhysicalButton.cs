using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PhysicalButton : MonoBehaviour {
	public UnityEvent Pressed;

	private void Start() {
		if(Pressed == null)
			Pressed = new UnityEvent();
	}

	private void OnTriggerEnter(Collider other) {
		if(other.CompareTag("UpAndDown")) {
			Pressed.Invoke();
			EventSystem.onClearKeyboard();
			EventSystem.onResetStringMatchChecker();
		}
	}
}
