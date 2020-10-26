using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DataSaver : MonoBehaviour {
	private int TotalErrorRaycast = 0, TotalErrorHeadHand = 0, TotalPhrasesLengthRaycast = 0, TotalPhrasesLengthHeadHand = 0, stringCount = 0;
	[SerializeField] private TextAsset errorRateAsset = null;

    List<string> LettersAndTimecode = new List<string>();
    
    private float timeSpendt = 0, finalTime = 0;
	private bool isTimeGoing = false;

	private void Awake() {
		EventSystem.onTypedCorrect += addTotalPhrasesLength;
		EventSystem.onTypedError += addTotalError;
		EventSystem.onSaveData += WriteToFile;
		EventSystem.onNextString += upStringCount;
		EventSystem.onButtonPressed += firstButtonPress;
        EventSystem.onButtonPressed += addLettersToDictionary;
        EventSystem.onBackspace += backspace;
	}

	void Update() {
		if(isTimeGoing)
			timeSpendt += Time.deltaTime;

		if(stringCount == 10) {
            EventSystem.onSaveData();
			isTimeGoing = false;
			EditorApplication.isPaused = true;
		}
	}

    void backspace()
    {
        LettersAndTimecode.Add(DateTime.Now.ToString() + " " + "BACKSPACE");
    }

    void addLettersToDictionary(char letter)
    {
            LettersAndTimecode.Add( DateTime.Now.ToString() + " " + letter.ToString());
    }

	public void addTotalError(bool isRaycastSecene) {
		if(isRaycastSecene) {
			++TotalErrorRaycast;
		} else {
			++TotalErrorHeadHand;
		}
	}

	public void addTotalPhrasesLength(bool isRaycastSecene) {
		if(isRaycastSecene) {
			++TotalPhrasesLengthRaycast;
		} else {
			++TotalPhrasesLengthHeadHand;
		}
	}

	private void firstButtonPress(char obj) {
		if(!isTimeGoing)
			isTimeGoing = true;
	}

	void upStringCount() {
		++stringCount;
	}

	[ContextMenu("WriteToFile")]
	public void WriteToFile() {
		string path = Application.persistentDataPath + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		StreamWriter writer = File.CreateText(path);
		writer.WriteLine(TotalErrorRaycast.ToString()
						+ ";" + TotalPhrasesLengthRaycast.ToString()
						+ ";" + TotalErrorHeadHand.ToString()
						+ ";" + TotalPhrasesLengthHeadHand.ToString());

        foreach (string letterTimePair in LettersAndTimecode)
        {
            writer.WriteLine(letterTimePair);
        }
		writer.Close();
		AssetDatabase.ImportAsset(path);
	}
}
