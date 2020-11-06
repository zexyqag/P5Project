using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeyboard : MonoBehaviour
{
    public float speed, max, min;
    public bool bUP;
    public GameObject Keyboard;

    private float fAngle;

    public bool c;

    private void Start()
    {
        fAngle = Keyboard.GetComponent<Transform>().eulerAngles.x;
    }

    private void Update()
    {
        if (c) // testing purposes
        {
            Rotate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("UpAndDown")){
            Rotate();
        }

    }

    void Rotate()
    {
        fAngle = Keyboard.GetComponent<Transform>().eulerAngles.x;
        Debug.Log(fAngle);

        if (bUP && fAngle < max)
        {
            fAngle += speed * Time.deltaTime;
            Keyboard.transform.eulerAngles = (new Vector3(fAngle, 0, 0));
        }
        else if (!bUP && fAngle < min)
        {
            fAngle += -speed * Time.deltaTime;
            Keyboard.transform.eulerAngles = (new Vector3(fAngle, 0, 0));
        }
    }
}
