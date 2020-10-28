using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeyboard : MonoBehaviour
{
    public float speed;
    public bool bUP;
    public bool bRotate;
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
        if (bUP)
        {
            Keyboard.transform.position += (new Vector3(0, speed * Time.deltaTime, 0));
        }
        else
        {
            Keyboard.transform.position += (new Vector3(0, -speed * Time.deltaTime, 0));
        }

    }

    void Rotate()
    {
        if (bUP)
        {
            Keyboard.transform.Rotate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else
        {
            Keyboard.transform.Rotate(new Vector3(-speed * Time.deltaTime, 0, 0));
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
