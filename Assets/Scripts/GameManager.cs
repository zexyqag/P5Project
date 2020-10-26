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

	public List<Transform> PlayerVariants;

	[ContextMenu("start")]
	private void Start() {
		//setAllToDefault();
		Debug.Log("gm.start");
		PlayerVariants.Shuffle();
		currentPlayerVariant = Instantiate(PlayerVariants[0]);
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
		Destroy(currentPlayerVariant.gameObject);
		currentPlayerVariant = Instantiate(PlayerVariants[1]);
	}
	/*
	public void LoadScene(SceneAsset sceneAsset) {
		LoadScene(sceneAsset.name);
	}

	public void LoadScene(string sceneNameOrPath) {
		int index = scenes.IndexOf((from s in scenes
									where s.path.Contains(sceneNameOrPath)
									select s).FirstOrDefault());
		if(index == -1) {
			Debug.Log("Could not find a scene with path or name: " + sceneNameOrPath.ToString());
		} else {
			LoadScene(index);
		}
	}

	public void LoadScene(int sceneIdx) {
		if(sceneIdx < 0 || sceneIdx >= scenes.Count) {
			Debug.Log("Could not find a scene with scene index: " + sceneIdx.ToString());
		} else {
			SceneManager.LoadSceneAsync(sceneIdx);
		}
	}
	*/
}
