﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour {

	public List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();
	public Transform PlayerPrefab;

	public UnityEvent onStart;

	private void Start() {
		if(PlayerPrefab == null) {
			Debug.Log("Missing PlayerPrefab");
		} else if(Player.instance == null) {
			Instantiate(PlayerPrefab);
		}
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if(scene.enabled)
				scenes.Add(scene);
		}
		onStart.Invoke();

	}

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
}
