using System;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour {
	#region Private fields
	private string filePath = string.Empty, testName = string.Empty;
	private StreamWriter writer = null;
	private bool isTestStarted = false, isReady = false;
	#endregion

	private void Awake() {
		EventSystem.onTypedCorrect += onTypedCorrect;
		EventSystem.onTypedError += addTotalError;
		EventSystem.onBackspace += addBackspace;
		EventSystem.onTestType += TestType;
		EventSystem.onSwtichInputMethod += endTest;
		EventSystem.onButtonPressed += startTest;
		EventSystem.onMissedButton += onMissedButton;

		OpenWriter();
	}

	private void onMissedButton() {
		if(isTestStarted) {
			logAction("MISSED;0;0");
		}
	}

	private void TestType(string testName) {
		this.testName = testName;
	}

	public void ReadyUp() {
		isReady = true;
	}

	private void OpenWriter() {
		if(writer == null) {
			filePath = $"{Application.persistentDataPath + Path.DirectorySeparatorChar + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.csv";
			writer = File.CreateText(filePath);
			writer.WriteLine("Imput Method;Time;Keypressed;Correct;Incorrect");
		}
	}
	public void addTotalError(char c) {
		if(isTestStarted) {
			logAction(c.ToString() + ";0;1");
		}
	}

	public void onTypedCorrect(char c) {
		if(isTestStarted) {
			logAction(c.ToString() + ";1;0");
		}
	}
	private void addBackspace() => logAction("BACKSPACE;0;0");

	private void startTest(char c) {
		if(!isReady || isTestStarted)
			return;

		isTestStarted = true;
		OpenWriter();
	}

	private void endTest() {
		isTestStarted = false;
		isReady = false;
	}

	private void logAction(string action) {
		if(writer.BaseStream != null) {
			writer.WriteLine(testName + ";" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + ";" + action);
		} else {
			Debug.Log("StreamWriter is closed");
		}
	}

	private void OnApplicationQuit() {
		closeWriter();
		Unsubscribe();
	}
	private void OnDisable() {
		closeWriter();
		Unsubscribe();
	}
	private void OnDestroy() {
		closeWriter();
		Unsubscribe();
	}

	/// <summary>
	/// Closes the writer ands saves the file
	/// </summary>
	private void closeWriter() {
		if(writer.BaseStream != null) {
			Debug.Log("file saved to: " + filePath.ToString());
			writer.Close();
		}
	}

	/// <summary>
	/// Unsubscribes the methods in this script from the EventSystem
	/// </summary>
	private void Unsubscribe() {
		EventSystem.onTypedCorrect -= onTypedCorrect;
		EventSystem.onTypedError -= addTotalError;
		EventSystem.onBackspace -= addBackspace;
		EventSystem.onTestType -= TestType;
		EventSystem.onSwtichInputMethod -= endTest;
		EventSystem.onButtonPressed -= startTest;
		EventSystem.onMissedButton -= onMissedButton;
	}
}
