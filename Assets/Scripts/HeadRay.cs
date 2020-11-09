using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRay : MonoBehaviour
{
    private GameObject CurrentButtonHower;
    private GameObject LastButton;


    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ButtonBehavior>())
        {
            other.GetComponent<ButtonBehavior>().HowerOver();
            CurrentButtonHower = other.gameObject;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (CurrentButtonHower != null && other.tag == "Hand")
        {
            //HitButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<ButtonBehavior>())
        {
            other.GetComponent<ButtonBehavior>().ButtonExit();
            CurrentButtonHower = null;
        }

        if (LastButton != null && other.tag == "Hand")
        {
            ReliseButton();
        }


    }

    private void HitButton()
    {

        if (CurrentButtonHower && CurrentButtonHower.GetComponent<ButtonBehavior>())
        {
            LastButton = CurrentButtonHower;
            CurrentButtonHower.GetComponent<ButtonBehavior>().OnButtonDown();
        } else {
            EventSystem.onMissedButton();
        }   

    }

    private void ReliseButton()
    {
        if (LastButton && LastButton.GetComponent<ButtonBehavior>())
        {
            LastButton.GetComponent<ButtonBehavior>().OnButtonUP();
            LastButton = null;

        }
    }
}
