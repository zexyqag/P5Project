using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlacer : MonoBehaviour
{
    public List<GameObject> keys;
    public float Spacing;
    public float sice;
    public GameObject SpaceBar, BackSpace;
    public Vector3 startPosition;

    private void Start()
    {
        sice = sice / 100;
        Scale();
        Place();

    }

    void Scale()
    {
        foreach (GameObject gameObject in keys)
        {
            gameObject.transform.localScale *= sice;
        }

        SpaceBar.transform.localScale *= sice;
        BackSpace.transform.localScale *= sice;
    }

    void Place()
    {
        for (int i = 0; i < 10; i++) // top row
        {
            Vector3 newPos = new Vector3(startPosition.x + (Spacing * i) + sice, startPosition.y, startPosition.z);
            keys[i].transform.position = newPos;
        }

        for (int i = 0; i < 9; i++) // middle row
        {
            Vector3 newPos = new Vector3(startPosition.x + (Spacing * i) + 0.05f + sice, startPosition.y - Spacing, startPosition.z);
            keys[i + 10].transform.position = newPos;
        }

        for (int i = 0; i < 7; i++) // button row
        {
            Vector3 newPos = new Vector3(startPosition.x + (Spacing * i) + 0.1f + sice, startPosition.y - Spacing * 2, startPosition.z);
            keys[i + 19].transform.position = newPos;
        }

        SpaceBar.transform.position = new Vector3(startPosition.x + Spacing + 0.3f + sice, startPosition.y - Spacing * 3, startPosition.z);
        BackSpace.transform.position = new Vector3(startPosition.x + Spacing + 0.6f + sice, startPosition.y - Spacing * 3 + 0.04f, startPosition.z);
    }

}
