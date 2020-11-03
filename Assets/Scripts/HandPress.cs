using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandPress : MonoBehaviour {

	private GameObject CurrentButtonHower;
	private GameObject LastButton;


	private void Update() {

	}

	private void OnTriggerStay(Collider other) {
		if(other.GetComponent<ButtonBehavior>()) {
			other.GetComponent<ButtonBehavior>().HowerOver();
		}
		CurrentButtonHower = other.gameObject;
	}

	private void OnTriggerExit(Collider other) {

		if(other.GetComponent<ButtonBehavior>()) {
			other.GetComponent<ButtonBehavior>().ButtonExit();
		}

		CurrentButtonHower = null;
	}

	public void HitButton() {

		if(CurrentButtonHower && CurrentButtonHower.GetComponent<ButtonBehavior>()) {
			LastButton = CurrentButtonHower;
			CurrentButtonHower.GetComponent<ButtonBehavior>().OnButtonDown();

		} else {
			EventSystem.onMissedButton();
		}

	}

	public void ReleaseButton() {
		if(LastButton && LastButton.GetComponent<ButtonBehavior>()) {
			LastButton.GetComponent<ButtonBehavior>().OnButtonUP();
			LastButton = null;

		}
	}


}

