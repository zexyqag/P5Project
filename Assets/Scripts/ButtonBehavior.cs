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

    private bool isHowerOver;

    private void Start()
    {
        LetterText.GetComponent<TextMesh>().text = Letter.ToString();
        ChangeMaterial(DefaultMaterial);
    }

    private void Update()
    {
        if (!isHowerOver)
        {
            ChangeMaterial(DefaultMaterial);
        }
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

     public void HowerMaterial()
     {
        ChangeMaterial(HowerOwer);
        isHowerOver = true;
        StartCoroutine(Wait());

    }

    void ChangeMaterial(Material newMaterial)
    {
        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void MoveButton(Vector3 moveAmount)
    {
        transform.Translate(moveAmount);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        isHowerOver = false;

    }

}
