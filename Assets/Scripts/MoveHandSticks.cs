using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandSticks : MonoBehaviour
{
    public GameObject Head;
    public GameObject Hand;

    private void Update()
    {
        MoveThis();
        LookAtHand();
    }


    void MoveThis()
    {
        Vector3 newPos = Head.GetComponent<Transform>().transform.position;
        this.transform.position = newPos;
    }

    void LookAtHand()
    {
        this.transform.LookAt(Hand.GetComponent<Transform>().transform.position);
    }
}
