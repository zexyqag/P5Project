using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataSaver : MonoBehaviour {
	private int TotalErrorRaycast = 0, TotalErrorHeadHand = 0, TotalPhrasesLengthRaycast = 0, TotalPhrasesLengthHeadHand = 0;
	[SerializeField] private TextAsset errorRateAsset;


	private void Start() {
		EventSystem.onTypedCorrect += addTotalPhrasesLength;
		EventSystem.onTypedError += addTotalError;
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
