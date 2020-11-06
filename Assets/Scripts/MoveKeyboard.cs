﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeyboard : MonoBehaviour
{
    public float speed, max, min;
    public bool bUP, bRotate;
    public GameObject Keyboard;

    private float fAngle;

    public bool m, c;

    private void Start()
    {
        //EventSystem.onButtonPressed += Deactivate;
        //EventSystem.onSwtichInputMethod += Activate;

        fAngle = Keyboard.GetComponent<Transform>().eulerAngles.x;
    }

    private void Update()
    {
        if (m)
        {
            //Move();
        }

        if (c)
        {
            Rotate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (bRotate)
        {
            case true:
                if (other.CompareTag("UpAndDown")){
                    Rotate();
                }
                break;

            case false:
                if (other.CompareTag("UpAndDown")){
                   // Move();
                }
                break;
        }
    }

    /*
    void Move()
    {
        Vector3 positionKeyboard = Keyboard.GetComponent<Transform>().position;

        if (bUP && positionKeyboard.y < max)
        {
            Keyboard.transform.position += (new Vector3(0, speed * Time.deltaTime, 0));
        }
        else if (!bUP && positionKeyboard.y > min)
        {
            Keyboard.transform.position += (new Vector3(0, -speed * Time.deltaTime, 0));
        }

    }
    */

    void Rotate()
    {
        fAngle = Keyboard.GetComponent<Transform>().eulerAngles.x;

        if (bUP)
        {
            fAngle += speed * Time.deltaTime;
            Keyboard.transform.localEulerAngles = (new Vector3(fAngle, 0, 0));
        }
        else if (!bUP)
        {
            fAngle += -speed * Time.deltaTime;
            Keyboard.transform.localEulerAngles = (new Vector3(fAngle, 0, 0));
        }
    }

    void Deactivate(char notInUse)
    {
        this.gameObject.SetActive(false);
    }

    void Activate()
    {
        this.gameObject.SetActive(true);
    }

}
