using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour {


	#region Public fields
	public UnityEvent TimerEnd;
	public float TimeLeft = 1;
	#endregion

	#region Private fields
	private float ResetTime = 0;
	private bool isDone = false, isReady = false, isTimerOn = false;
	#endregion

	//Subscribe to relvent events and save which time to reset to
	private void Awake() {
		EventSystem.onButtonPressed += StartCountdown;
		ResetTime = TimeLeft;
	}

	/// <summary>
	/// Ready CountDown to start counting down on first keystroke
	/// </summary>
	public void ReadyUp() {
		isReady = true;
	}

	/// <summary>
	/// Start countdown when player press the first keystroke and if CountDown is ready
	/// </summary>
	public void StartCountdown(char c) {
		if(!isTimerOn && isReady) {
			isReady = false;
			isTimerOn = true;
			StartCoroutine(wait());
		}
	}

	//IEnumerator to countdown on separate thread and restart test with new input method when over
	IEnumerator wait() {
		while(TimeLeft > 0) {
			TimeLeft -= Time.deltaTime;
			yield return null;
		}
		isTimerOn = false;
		TimeLeft = ResetTime;


		EventSystem.onSwtichInputMethod();
		EventSystem.onNextString();
		EventSystem.onSetPos();
		TimerEnd.Invoke();

		//If this is the second time load the end scene
		if(isDone) {
			SceneManager.LoadScene(2);
		}

		isDone = true;

	}

	private void OnApplicationQuit() => Unsubscribe();
	private void OnDisable() => Unsubscribe();
	private void OnDestroy() => Unsubscribe();

	/// <summary>
	/// Unsubscribes the methods in this script from the EventSystem
	/// </summary>
	private void Unsubscribe() {
		EventSystem.onButtonPressed -= StartCountdown;
	}
}
