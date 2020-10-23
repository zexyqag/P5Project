using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class SceneLoader {
	static public List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();

	static private void initlizeSceneList() {
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if(scene.enabled)
				scenes.Add(scene);
		}
	}

	static public void LoadScene(SceneAsset sceneAsset) {
		LoadScene(sceneAsset.name);
	}

	static public void LoadScene(string sceneNameOrPath) {
		if(scenes.Count == 0)
			initlizeSceneList();

		int index = scenes.IndexOf((from s in scenes
									where s.path.Contains(sceneNameOrPath)
									select s).FirstOrDefault());
		if(index == -1) {
			Debug.Log("Could not find a scene with path or name: " + sceneNameOrPath.ToString());
		} else {
			LoadScene(index);
		}
	}

	static public void LoadScene(int sceneIdx) {
		if(sceneIdx < 0 || sceneIdx >= scenes.Count) {
			Debug.Log("Could not find a scene with scene index: " + sceneIdx.ToString());
		} else {
			SceneManager.LoadSceneAsync(sceneIdx);
		}
	}
}
