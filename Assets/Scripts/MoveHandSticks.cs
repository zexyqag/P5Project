using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandSticks : MonoBehaviour
{
    public GameObject Head;

    private void Update()
    {
        MoveThis();
    }


    void MoveThis()
    {
        this.transform.position = Head.GetComponent<Transform>().transform.position;
    }
}
