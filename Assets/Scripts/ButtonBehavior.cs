using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonBehavior : MonoBehaviour
{
    #region Public fields
    public char Letter;
    public GameObject LetterText;

    public Material HowerOwer;
    public Material DefaultMaterial;
    public Material PressMaterial;

    public Vector3 AmountToMove;
    #endregion

    private void Start()
    {
        LetterText.GetComponent<TextMesh>().text = Letter.ToString();
        ChangeMaterial(DefaultMaterial);
    }

    public void OnButtonDown()
    {
        if(Letter == '<') {
            EventSystem.onBackspace();
        } else {
            EventSystem.onButtonPressed(Letter);
        }
        Debug.Log("OnButtonPressed");
        MoveButton(AmountToMove);
        ChangeMaterial(PressMaterial);
    }

    public void OnButtonExit()
    {
        MoveButton(-AmountToMove);
        ChangeMaterial(DefaultMaterial);
    }

    void ChangeMaterial(Material newMaterial)
    {
        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void MoveButton(Vector3 moveAmount)
    {
        transform.Translate(moveAmount);
    }

}
