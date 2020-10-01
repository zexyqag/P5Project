﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPress : MonoBehaviour {



	private GameObject CurrentButtonHower;
    private GameObject LastButton;


	private void Update() {

	}

    private void OnTriggerEnter(Collider other)
    {
		CurrentButtonHower = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentButtonHower = null;
    }

    [ContextMenu("HitButton")]
	public void HitButton() {

        if (CurrentButtonHower && CurrentButtonHower.GetComponent<ButtonBehavior>())
        {
            LastButton = CurrentButtonHower;
            CurrentButtonHower.GetComponent<ButtonBehavior>().OnButtonDown();
        }

	}

	public void ReliseButton() {
        if (LastButton && LastButton.GetComponent<ButtonBehavior>())
        {
            LastButton.GetComponent<ButtonBehavior>().OnButtonUP();
            LastButton = null;
            
        }
    }
}
