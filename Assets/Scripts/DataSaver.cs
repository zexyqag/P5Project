using System;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour {
	private int TotalError = 0, TotalPhrasesLength = 0;
	private string filePath = string.Empty, testName = string.Empty;
	private StreamWriter writer = null;

	private void Awake() {
		EventSystem.onTypedCorrect += onTypedCorrect;
		EventSystem.onTypedError += addTotalError;
		EventSystem.onBackspace += addBackspace;
		EventSystem.onTestType += startTest;
		EventSystem.onSwtichInputMethod += endTest;
		filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + ".csv";
		writer = File.CreateText(filePath);
	}

	public void addTotalError(char c) {
		logAction(c.ToString()+ ";ERROR");
		++TotalError;
	}

	public void onTypedCorrect(char c) {
		logAction(c.ToString() + ";CORRECT");
		++TotalPhrasesLength;
	}
	private void addBackspace() => logAction("BACKSPACE");

	private void startTest(string testName) {
		this.testName = testName;
		TotalError = 0;
		TotalPhrasesLength = 0;
		writer.WriteLine("Player;When;Action;Additional");
		logAction("START");
	}

	private void endTest() => logAction("END;" + TotalError.ToString() + ";" + TotalPhrasesLength.ToString());

	private void logAction(string action) {
		if(writer.BaseStream != null) {
			//Debug.Log("Logged: " + action);
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
