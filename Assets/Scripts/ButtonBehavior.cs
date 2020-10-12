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
    public Material MatDefault;
    public Material MatPress;

    public Vector3 PosDefoult;
    public Vector3 PosHower;
    public Vector3 PosPress;

    public bool isPressDown;

    public AudioPlayer audioPlayer;
    #endregion

    private bool isHover;


    private void Start()
    {
        LetterText.GetComponent<TextMesh>().text = Letter.ToString();
        ChangeMaterial(MatDefault);

        setPositions();
        isPressDown = false;
    }

    private void setPositions()
    {
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
        audioPlayer.playAudio();
        MoveButton(PosPress);
        ChangeMaterial(MatPress);
        isPressDown = true;
    }

    public void OnButtonUP()
    {
        if (isHover)
        {
            ChangeMaterial(MatHower);
            MoveButton(PosHower);
        }
        else
        {
            ChangeMaterial(MatDefault);
            MoveButton(PosDefoult);
        }
        isPressDown = false;

    }

    public void HowerOver()
    {
        if (!isHover && !isPressDown)
        {
            ChangeMaterial(MatHower);
            MoveButton(PosHower);
        }
        isHover = true;
    }

    public void ButtonExit()
    {
        isHover = false;

        if (!isPressDown)
        {
            ChangeMaterial(MatDefault);
            MoveButton(PosDefoult);
        }
        
    }


    void ChangeMaterial(Material newMaterial)
    {
        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void MoveButton(Vector3 newPos)
    {
        
        this.transform.parent.position = newPos;
    }

}
