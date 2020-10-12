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
	private float timeSpendt = 0, finalTime = 0;
	private bool isTimeGoing = false;

	private void Awake() {
		EventSystem.onTypedCorrect += addTotalPhrasesLength;
		EventSystem.onTypedError += addTotalError;
		EventSystem.onSaveData += WriteToFile;
		EventSystem.onNextString += upStringCount;
		EventSystem.onButtonPressed += firstButtonPress;
	}

	void Update() {
		if(isTimeGoing)
			timeSpendt += Time.deltaTime;

		if(stringCount == 10) {
			isTimeGoing = false;
			EditorApplication.isPaused = true;
		}
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
		if(!errorRateAsset) {
			Debug.Log("TextAsset missing on: " + this);
			return;
		}

		string path = AssetDatabase.GetAssetPath(errorRateAsset);
		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine(System.DateTime.Now.ToString()
						+ ";" + TotalErrorRaycast.ToString()
						+ ";" + TotalPhrasesLengthRaycast.ToString()
						+ ";" + TotalErrorHeadHand.ToString()
						+ ";" + TotalPhrasesLengthHeadHand.ToString());
		writer.Close();
		AssetDatabase.ImportAsset(path);
	}
}
