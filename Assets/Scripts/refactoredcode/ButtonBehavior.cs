using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonBehavior : MonoBehaviour
     //This script handels the visual and backend interactions of a button
{
    #region Public fields
    public char Letter;                         // The charrecter represinting the letter of the button
    public GameObject TextObject;               // A 3D text gameobject used to desplay the letter of the button
   
    public Material HowerMaterial;              // Material of the button when it is howerd over
    public Material DefaultMaterial;            // Material of the button when it is not howerd over of pressed
    public Material PressdownMaterial;          // Material of the button when it is pressed

    public Vector3 DefoultPosition;             // The initial position of the button, and the position it will retun to after moving

    public AudioPlayer audioPlayer;             // The global audio payer used for handeling sound
    #endregion

    #region Private Fields
    private Vector3 HoverOverPosition;          // The position of the button when it is hoverd over, in relation to the defoult position
    private Vector3 PressDownPosition;          // The position of the button when it is pressed down, in relation to the defoult position

    private bool isHover;                       // A boolean indicating weather or not the button icurrently being hover over
    private bool isThisPressedDown;             // A boolean indicating weather or not the button icurrently being pressed
    #endregion

    #region Unity Functions
    private void Start()
    {
        EventSystem.onSetPos += SetPositions;     
        
        SetPositions();

        isThisPressedDown = false;
    }
    #endregion

    private void SetPositions()
    {
        TextObject.GetComponent<TextMesh>().text = Letter.ToString();       // Sets the text component of the 3D text object to the letter char
        ChangeMaterial(DefaultMaterial);

        DefoultPosition = this.transform.parent.localPosition;              // Sets the defoult position to the current position of the button
        HoverOverPosition = DefoultPosition + new Vector3(0,0, -0.03f);     // Sets the hover ower and press down postion based on the defoult position
        PressDownPosition = DefoultPosition + new Vector3(0, 0, 0.03f);

    }

    #region ButtonInteraction
    public void OnButtonDown()
    {
        if (Letter == '<') { EventSystem.onBackspace(); } else { EventSystem.onButtonPressed(Letter); } // Checks if the letter input is backspace and invokes the corect event

        audioPlayer.playAudio();
        MoveButton(PressDownPosition);
        ChangeMaterial(PressdownMaterial);
        isThisPressedDown = true;
    }

    public void OnButtonUP()
    {
        if (isHover)
        {
            ChangeMaterial(HowerMaterial);
            MoveButton(HoverOverPosition);
        }
        else
        {
            ChangeMaterial(DefaultMaterial);
            MoveButton(DefoultPosition);
        }

        isThisPressedDown = false;

    }

    public void HowerOver()
    {
        if (!isHover && !isThisPressedDown)
        {
            ChangeMaterial(HowerMaterial);
            MoveButton(HoverOverPosition);
        }

        isHover = true;
    }

    public void ButtonExit()
    {
        isHover = false;

        if (!isThisPressedDown)
        {
            ChangeMaterial(DefaultMaterial);
            MoveButton(DefoultPosition);
        } 
    }
    #endregion

    #region Set Button component variables

    void ChangeMaterial(Material newMaterial)
    {
        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void MoveButton(Vector3 newPos)
    {
        
        this.transform.parent.localPosition = newPos;
    }

    #endregion

    #region Unsubscripes
    private void OnApplicationQuit() => Unsubscribe();
    private void OnDisable() => Unsubscribe();
    private void OnDestroy() => Unsubscribe();
    private void Unsubscribe() {
        EventSystem.onSetPos -= SetPositions;
    }
    #endregion
}
