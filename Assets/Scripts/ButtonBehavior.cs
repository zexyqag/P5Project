using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonBehavior : MonoBehaviour
{
    #region Public fields
    public char Letter;
    public GameObject LetterText;

    public Material MatHower;
    public Material MatDefoult;
    public Material MatPress;

    public Vector3 PosDefoult;
    public Vector3 PosHower;
    public Vector3 PosPress;
    #endregion

    private bool isHower;


    private void Start()
    {
        LetterText.GetComponent<TextMesh>().text = Letter.ToString();
        ChangeMaterial(MatDefoult);

        PosDefoult = this.transform.position;
        PosHower = PosHower + PosDefoult;
        PosPress = PosPress + PosDefoult;
    }

    public void OnButtonDown()
    {
        if(Letter == '<') {
            EventSystem.onBackspace();
        } else {
            EventSystem.onButtonPressed(Letter);
        }
        MoveButton(PosPress);
        ChangeMaterial(MatPress);
    }

    public void OnButtonUP()
    {
        if (isHower)
        {
            ChangeMaterial(MatHower);
            MoveButton(PosHower);
        }
        else
        {
            ChangeMaterial(MatDefoult);
            MoveButton(PosDefoult);
        }

    }

    public void HowerOver()
    {
        isHower = true;
        ChangeMaterial(MatHower);
        MoveButton(PosHower);
    }

    public void ButtonExit()
    {
        isHower = false;
        ChangeMaterial(MatDefoult);
        MoveButton(PosDefoult);
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
