using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPress : MonoBehaviour
{


    RaycastHit hit;
    private GameObject lastButtonPressed;

    private void Update()
    {
        Vector3 rayDirection = (transform.forward + transform.forward + transform.up).normalized;
        Debug.DrawRay(this.transform.position, rayDirection * 10, Color.yellow);

        Physics.Raycast(this.transform.position, rayDirection, out hit, 10);

        if (hit.transform.gameObject.GetComponent<ButtonBehavior>())
        {
            hit.transform.gameObject.GetComponent<ButtonBehavior>().HowerMaterial();
        }

    }

    [ContextMenu("HitButton")]
    public void HitButton()
    {
        if (!hit.transform.gameObject.GetComponent<ButtonBehavior>()) return;

        hit.transform.gameObject.GetComponent<ButtonBehavior>().OnButtonDown();
        lastButtonPressed = hit.transform.gameObject;


    }

    public void ReliseButton()
    {
        if (lastButtonPressed.GetComponent<ButtonBehavior>())
        {
            lastButtonPressed.GetComponent<ButtonBehavior>().OnButtonExit();
        }
        lastButtonPressed = null;
    }
}
