using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPress : MonoBehaviour {


	RaycastHit hit;
	private ButtonBehavior lastButtonPressed, currentButtonHovered;

	private void Update() {
		Vector3 rayDirection = (transform.forward + transform.forward + transform.up).normalized;
		Debug.DrawRay(this.transform.position, rayDirection * 10, Color.yellow);

		if(Physics.Raycast(this.transform.position, rayDirection, out hit, 10)) {
			if(hit.transform.gameObject.GetComponent<ButtonBehavior>()) {
				currentButtonHovered = hit.transform.gameObject.GetComponent<ButtonBehavior>();
				currentButtonHovered.HowerMaterial();
			} else {
				currentButtonHovered = null;
			}
		}
	}

	[ContextMenu("HitButton")]
	public void HitButton() {
		if(!currentButtonHovered)
			return;

		currentButtonHovered.OnButtonDown();
		lastButtonPressed = currentButtonHovered;


	}

	public void ReliseButton() {
		if(lastButtonPressed) {
			lastButtonPressed.OnButtonExit();
			lastButtonPressed = null;
		}
	}
}
