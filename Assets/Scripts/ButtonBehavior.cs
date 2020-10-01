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

    public bool isPressDown;
    #endregion

    private bool isHower;


    private void Start()
    {
        LetterText.GetComponent<TextMesh>().text = Letter.ToString();
        ChangeMaterial(MatDefoult);

        PosDefoult = this.transform.position;
        PosHower = PosHower + PosDefoult;
        PosPress = PosPress + PosDefoult;
        isPressDown = false;
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
        isPressDown = true;
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
        isPressDown = false;

    }

    public void HowerOver()
    {
        if (!isHower && !isPressDown)
        {
            ChangeMaterial(MatHower);
            MoveButton(PosHower);
        }
        isHower = true;
    }

    public void ButtonExit()
    {
        isHower = false;

        if (!isPressDown)
        {
            ChangeMaterial(MatDefoult);
            MoveButton(PosDefoult);
        }
        
    }


    void ChangeMaterial(Material newMaterial)
    {
        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void MoveButton(Vector3 newPos)
    {
        this.transform.position = newPos;
    }

}
