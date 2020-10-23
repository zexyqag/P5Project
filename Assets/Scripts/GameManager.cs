using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour {

	public gameSettingsScriptableObject gameSettings;
	public Transform Player;
	public bool isCurrentTestHeadHand = false;
	public int currentTestNumber = 0;
	static public GameManager Instance { get; private set; }

	private void Awake() {
		if(!Instance) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}

		if(!gameSettings) {
			Debug.Log("No game settings assinged, yeeting " + gameObject.name);
			Destroy(gameObject);
			return;
		}

		Player = Instantiate(gameSettings.ShouldStartWithHeadHand ? gameSettings.HeadHandPlayer : gameSettings.HandPlayer);
		isCurrentTestHeadHand = gameSettings.ShouldStartWithHeadHand;
		SceneLoader.LoadScene(gameSettings.AdjustmentScene);
	}

	private void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
	}

	public void LoadAdjustmentScene() {
		if(currentTestNumber > gameSettings.testAmountPerScene * 2)
			Application.Quit();

		Destroy(Player);
		isCurrentTestHeadHand = !isCurrentTestHeadHand;
		SceneLoader.LoadScene(gameSettings.AdjustmentScene);
		Player = Instantiate(!isCurrentTestHeadHand ? gameSettings.HeadHandPlayer : gameSettings.HandPlayer);
	}

	public void LoadNextTest() {
		SceneLoader.LoadScene(gameSettings.TestingScene);
	}

	private void setAllToDefault() {
		GameObject[] allObjects = FindObjectsOfType<GameObject>();
		foreach(GameObject go in allObjects) {
			if(go.layer != 8 || go.layer != 9 || go.layer != 10) {
				go.layer = 0;

				if(go.tag != "Button") {
					go.layer = 0;
				}
			}
		}
	}


	private void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
