using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBoard : MonoBehaviour
{
    private Text textFlied;
    private void Start()
    {
        EventSystem.onButtonPressed += AddText;
        textFlied = this.GetComponent<UnityEngine.UI.Text>();
        FlashIndicator();
        
    }

    void AddText(char letterToAdd)
    {
        if (letterToAdd.Equals('<')) 
        {
            DeleteText();
            return;
        }

        textFlied.text = textFlied.text.Substring(0, textFlied.text.Length -1);
        textFlied.text += letterToAdd;
        textFlied.text += "|";
    }

    void DeleteText()
    {
        if (textFlied.text.Length <= 1) { return; }

        textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1);
        
    }

    void FlashIndicator()
    {
        StartCoroutine(WaitforTime(0.5f));
    }

    IEnumerator WaitforTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1) + " ";
        
        yield return new WaitForSeconds(timeToWait);
        textFlied.text = textFlied.text.Substring(0, textFlied.text.Length - 1) + "|";

        FlashIndicator();
    }
}
