using UnityEngine;


public class HandPress : MonoBehaviour 
{

    #region Private Fields
    private GameObject CurrentButtonHower;	// The button that this is currently coliding with
	private GameObject LastButton;          // The last button this hit
    #endregion

    #region Trigger detections
    private void OnTriggerStay(Collider other) 
	{// This function is called every frame this is coliding with a trigger

		if(other.GetComponent<ButtonBehavior>())				// If hovering over a button
			other.GetComponent<ButtonBehavior>().HowerOver();	// Call that buttons hover over function

		CurrentButtonHower = other.gameObject;
	}

	private void OnTriggerExit(Collider other)
	{// This function is called ones, when this exits a trigger

		if(other.GetComponent<ButtonBehavior>())				// If this exites from a button
			other.GetComponent<ButtonBehavior>().ButtonExit();	// Call the exit function of that button

		CurrentButtonHower = null;
	}

    #endregion

    public void HitButton() 
	{// This function is called trough a Steam-VR scripts, that detects when the trigger button is pressed on the controler

		if(CurrentButtonHower && CurrentButtonHower.GetComponent<ButtonBehavior>())	// If hovering over somthing and that somthing is a button
		{
			LastButton = CurrentButtonHower;										
			CurrentButtonHower.GetComponent<ButtonBehavior>().OnButtonDown();		// Call the button down function on that button

		} 
		else 
			EventSystem.onMissedButton();											// If the trigger button is pressed anywhere not on a button
	}

	public void ReleaseButton() 
	{
		if(LastButton && LastButton.GetComponent<ButtonBehavior>())	// If last button is not empty (a button has bin pressed but not released) and it a button
		{
			LastButton.GetComponent<ButtonBehavior>().OnButtonUP(); // Call the relese button function on that button
			LastButton = null;
		}
	}
}

