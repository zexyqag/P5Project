using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataSaver : MonoBehaviour {
	private int TotalErrorRaycast = 0, TotalErrorHeadHand = 0, TotalPhrasesLengthRaycast = 0, TotalPhrasesLengthHeadHand = 0;
	[SerializeField] private TextAsset errorRateAsset;

	public void WriteToFile() {
		if(!errorRateAsset) {
			Debug.Log("TextAsset missing on: " + this);
			return;
		}

		string path = AssetDatabase.GetAssetPath(errorRateAsset);
		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine(System.DateTime.Now.ToString()
						+ ";" + TotalErrorRaycast.ToString()
						+ ";" + TotalErrorHeadHand.ToString()
						+ ";" + TotalPhrasesLengthRaycast.ToString()
						+ ";" + TotalPhrasesLengthHeadHand.ToString());
		writer.Close();
		AssetDatabase.ImportAsset(path);
	}
}
