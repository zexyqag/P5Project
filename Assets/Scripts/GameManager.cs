using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour {

	public List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();
	private Transform currentPlayerVariant;

	public UnityEvent onStart;

	public List<Transform> playerVariants;
	public List<Vector2> keyboards;
	private List<KeyValuePair<Transform, Vector2>> PlayerAndKeyboardVariants = new List<KeyValuePair<Transform, Vector2>>();

	private bool isBothPlayerVerants = false; // bool is true if both player imput types has bin testet

	[ContextMenu("start")]
	private void Start() {

		EventSystem.onSwtichInputMethod += switchInputType;

		for (int i = 0; i < playerVariants.Count; i++)
		{
			PlayerAndKeyboardVariants.Add(new KeyValuePair<Transform, Vector2>(playerVariants[i], keyboards[i]));
		}

		PlayerAndKeyboardVariants.Shuffle();
		setPlayerAndKeyboardPos(0);
		onStart.Invoke();
	}

	[ContextMenu("set the positions of all buttons")]
	private void setAllPositionsOfButtons()
    {
		EventSystem.onSetPos();
    }

	private void setAllToDefault()
    {
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject go in allObjects)
        {
			if(go.layer != 8 || go.layer != 9 || go.layer != 10)
            {
				go.layer = 0;

				if (go.tag != "Button")
				{
					go.layer = 0;
				}
			}
		}
	}

	[ContextMenu("switch imput type")]
	public void switchInputType()
    {
        if (isBothPlayerVerants)
        {
			return;
        }
		Destroy(currentPlayerVariant.gameObject);
		setPlayerAndKeyboardPos(1);
		isBothPlayerVerants = true;
	}

	private void setPlayerAndKeyboardPos(int element)
    {
		EventSystem.onTestType(PlayerAndKeyboardVariants[element].Key.name);
		currentPlayerVariant = Instantiate(PlayerAndKeyboardVariants[element].Key);
		KeyboardPlacer KP = this.GetComponent<KeyboardPlacer>();
		KP.Size = PlayerAndKeyboardVariants[element].Value.x;
		KP.Spacing = PlayerAndKeyboardVariants[element].Value.y;
		KP.Place();
	}

	[ContextMenu("onButtonPressed")]
	void activateButtonPress()
    {
		EventSystem.onButtonPressed('A');
    }
}
