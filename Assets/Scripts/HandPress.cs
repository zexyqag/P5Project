using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPress : MonoBehaviour
{

    public Vector3 rayDirection;

    RaycastHit hit;
    private GameObject lastButtonPressed;

    private void Update()
    {

        Debug.DrawRay(this.transform.position, rayDirection * 1, Color.yellow);

        Physics.Raycast(this.transform.position, rayDirection, out hit, 1);
        
    }

    public void HitButton()
    {
        if (!hit.transform.gameObject.GetComponent<ButtonBehavior>()) return;

        hit.transform.gameObject.GetComponent<ButtonBehavior>().OnButtonDown();
        lastButtonPressed = hit.transform.gameObject;


    }

    public void ReliseButton()
    {
        lastButtonPressed.GetComponent<ButtonBehavior>().OnButtonExit();
    }
}
