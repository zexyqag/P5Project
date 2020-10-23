using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/gameSettingsScriptableObject", order = 1)]
public class gameSettingsScriptableObject : ScriptableObject {
	public bool ShouldStartWithHeadHand;
	public float RotationAroundPlayer, RrotationAroundSelf;
	public int testAmountPerScene;
	public SceneAsset AdjustmentScene, TestingScene;
	public Transform HeadHandPlayer, HandPlayer;
}
