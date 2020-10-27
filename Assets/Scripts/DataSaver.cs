using System;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour {
	private int TotalError = 0, TotalPhrasesLength = 0;
	private string filePath = string.Empty, testName = string.Empty;
	private StreamWriter writer = null;

	private void Awake() {
		EventSystem.onTypedCorrect += addTotalPhrasesLength;
		EventSystem.onTypedError += addTotalError;
		EventSystem.onButtonPressed += addButtonPressed;
		EventSystem.onBackspace += addBackspace;
		EventSystem.onTestType += startTest;
		EventSystem.onSwtichInputMethod += endTest;
		filePath = Application.persistentDataPath + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + ".csv";
		writer = File.CreateText(filePath);
	}

	//private void Start() => 

	public void addTotalError() => ++TotalError;

	public void addTotalPhrasesLength() => ++TotalPhrasesLength;

	private void addButtonPressed(char c) => logAction(c.ToString());

	private void addBackspace() => logAction("BACKSPACE");

	private void startTest(string testName) {
		this.testName = testName;
		TotalError = 0;
		TotalPhrasesLength = 0;
		logAction("START");
	}

	private void endTest() => logAction("END" + ";" + TotalError.ToString() + ";" + TotalPhrasesLength.ToString());

	private void logAction(string action) {
		if(writer.BaseStream != null) {
			writer.WriteLine(testName + ";" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + ";" + action);
		} else {
			Debug.Log("StreamWriter is closed");
		}
	}

	private void OnApplicationQuit() => closeWriter();
	private void OnDisable() => closeWriter();
	private void OnDestroy() => closeWriter();
	private void closeWriter() {
		if(writer.BaseStream != null) {
			Debug.Log("file saved to: " + filePath.ToString());
			writer.Close();
		}
	}
}
