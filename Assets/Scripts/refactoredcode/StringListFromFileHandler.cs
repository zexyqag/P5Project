using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StringListFromFileHandler : MonoBehaviour {

	#region Private var
	[SerializeField] private TextAsset phrasesToWriteAsset = null;
	private List<string> phrasesToWriteStringList = new List<string>();
	private int phraseIndex = 0;
	private Text textField = null;
	private string currentString = string.Empty;
	#endregion

	private void Awake() {
		EventSystem.onNextString += nextText;
		if(phrasesToWriteAsset) {
			using(System.IO.StringReader reader = new System.IO.StringReader(phrasesToWriteAsset.text)) {
				string line;
				while((line = reader.ReadLine()) != null) {
					phrasesToWriteStringList.Add(line);
				}
			}
		} else {
			Debug.LogError("No text file assigned to: " + this + " on " + gameObject.name);
			phrasesToWriteStringList = new List<string> { "No text file assigned", "You forgot to assgin a text file", "Missing text file", "Text file be gone" };
		}
	}

	//Get nessesary refrence to game componets and initialize the string list
	private void Start() {
		textField = GetComponent<Text>();
		Initialize();
	}

	/// <summary>
	/// Shuffles the list, resets the index to zero, and calls nextText()
	/// </summary>
	private void Initialize() {
		phraseIndex = 0;
		phrasesToWriteStringList.Shuffle();
		nextText();
	}

	/// <summary>
	/// Updates the current string to match with the next string from the string list, index exceeds string list length Initialize is called before trying again.
	/// </summary>
	[ContextMenu("nextText()")]
	public void nextText() {
		if(phraseIndex >= phrasesToWriteStringList.Count) {
			Initialize();
		} else {
			currentString = phrasesToWriteStringList.ElementAt(phraseIndex).ToUpper();
			++phraseIndex;
		}
		textField.text = currentString;
		EventSystem.onUpdateStringToMatch(currentString);
	}

	private void OnApplicationQuit() => Unsubscribe();
	private void OnDisable() => Unsubscribe();
	private void OnDestroy() => Unsubscribe();

	/// <summary>
	/// Unsubscribes the methods in this script from the EventSystem
	/// </summary>
	private void Unsubscribe() {
		EventSystem.onNextString -= nextText;
	}
}
