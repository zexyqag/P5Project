using System;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour {
	#region Private fields
	private string testName = string.Empty;
	private StreamWriter writer = null;
	private bool isTestStarted = false, isReady = false;
	private DirectoryInfo dataDirectory = null;
	private FileInfo dataFile = null;
	#endregion

	private void Awake() {
		EventSystem.onTypedCorrect += onTypedCorrect;
		EventSystem.onTypedError += addTotalError;
		EventSystem.onBackspace += addBackspace;
		EventSystem.onTestType += TestType;
		EventSystem.onSwtichInputMethod += endTest;
		EventSystem.onButtonPressed += startTest;
		EventSystem.onMissedButton += onMissedButton;


		dataDirectory = Directory.CreateDirectory(Path.Combine(
			Application.isEditor ? Application.persistentDataPath : Application.dataPath,
			"testData"));

		OpenWriter();
	}

	/// <summary>
	/// Log when user missed a key
	/// </summary>
	private void onMissedButton() {
		if(isTestStarted) {
			logAction("MISSED;0;0");
		}
	}

	/// <summary>
	/// Sets the type of test as a string
	/// </summary>
	private void TestType(string testName) {
		this.testName = testName;
	}

	/// <summary>
	/// Redies the DataServer to start logging on the first keystroke
	/// </summary>
	public void ReadyUp() {
		isReady = true;
	}

	/// <summary>
	/// Opens the writer if it isnt already and saves the logged data to a new file
	/// </summary>
	private void OpenWriter() {
		if(writer == null) {
			dataFile = new FileInfo(Path.Combine(dataDirectory.FullName, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() + ".csv"));
			writer = File.CreateText(dataFile.FullName);
			writer.WriteLine("ImputMethod;Time;Keypressed;Correct;Incorrect");
		}
	}

	/// <summary>
	/// Loggs when the user presses an incorrect keystroke 
	/// </summary>
	public void addTotalError(char c) {
		if(isTestStarted) {
			logAction(c.ToString() + ";0;1");
		}
	}

	/// <summary>
	/// Loggs when the user presses an correct keystroke 
	/// </summary>
	public void onTypedCorrect(char c) {
		if(isTestStarted) {
			logAction(c.ToString() + ";1;0");
		}
	}

	/// <summary>
	/// Loggs when the user presses backspace
	/// </summary>
	private void addBackspace() => logAction("BACKSPACE;0;0");

	/// <summary>
	/// Starts the test when user presses a keyboard key if the data saver is ready
	/// </summary>
	private void startTest(char c) {
		if(!isReady || isTestStarted)
			return;

		isTestStarted = true;
		OpenWriter();
	}

	/// <summary>
	/// Stops the data saver from logging 
	/// </summary>
	private void endTest() {
		isTestStarted = false;
		isReady = false;
	}

	/// <summary>
	/// Logs and action as a new line with test type and time stamp
	/// </summary>
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
			Debug.Log("file saved to: " + dataFile.FullName);
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
