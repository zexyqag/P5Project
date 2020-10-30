using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeyboard : MonoBehaviour
{
    public float speed, max, min;
    public bool bUP, bRotate;
    public GameObject Keyboard;

    private void Start()
    {
        EventSystem.onButtonPressed += Deactivate;
        EventSystem.onSwtichInputMethod += Activate;
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
                    Move();
                }
                break;
        }
    }

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

    void Rotate()
    {
        Quaternion rotationKeyboard = Keyboard.GetComponent<Transform>().rotation;

        if (bUP && rotationKeyboard.x < max)
        {
            Keyboard.transform.Rotate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else if (!bUP && rotationKeyboard.x > min)
        {
            Keyboard.transform.Rotate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
    }

    void Deactivate(char notInUse)
    {
        this.gameObject.SetActive(false);
        Debug.Log(this.gameObject);
    }

    void Activate()
    {
        this.gameObject.SetActive(true);
    }

}
