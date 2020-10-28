using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeyboard : MonoBehaviour
{
    public float speed;
    public bool bUP;
    public GameObject Keyboard;

    void Move()
    {
        if (bUP)
        {
            Keyboard.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        else
        {
            Keyboard.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
          
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "UpAndDown")
        {
            Move();
        }
    }

}
