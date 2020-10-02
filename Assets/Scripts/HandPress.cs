using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem
{
    public class HandPress : MonoBehaviour
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
            }
            CurrentButtonHower = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {

            if (other.GetComponent<ButtonBehavior>())
            {
                other.GetComponent<ButtonBehavior>().ButtonExit();
            }

            CurrentButtonHower = null;
        }

        [ContextMenu("HitButton")]
        public void HitButton()
        {

            if (CurrentButtonHower.GetComponent<ButtonBehavior>())
            {
                LastButton = CurrentButtonHower;
                CurrentButtonHower.GetComponent<ButtonBehavior>().OnButtonDown();
                VibrateHand(10000);

            }

        }

        public void ReliseButton()
        {
            if (LastButton && LastButton.GetComponent<ButtonBehavior>())
            {
                LastButton.GetComponent<ButtonBehavior>().OnButtonUP();
                LastButton = null;

            }
        }

        void VibrateHand(ushort vibrationInMilisecons)
        {

            Hand hand = this.GetComponentInParent<Hand>();
            if (hand != null)
            {
                Debug.Log("vibrate");
                //hand.TriggerHapticPulse(vibrationInMilisecons);
                
            }
        }


    }
}

