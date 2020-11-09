using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour {
	public UnityEvent TimerEnd;
	public float TimeLeft = 1;
	private float ResetTime = 0;
	private bool isTimerOn = false;
	private bool isDone = false;
	public bool isReady = false;

	private void Awake() {
		EventSystem.onButtonPressed += StartCountdown;
		ResetTime = TimeLeft;
	}

	public void StartCountdown(char c) {
		if(!isTimerOn && isReady) {
			isReady = false;
			isTimerOn = true;
			StartCoroutine(wait());
		}
	}
	public void ReadyUp() {
		isReady = true;
	}

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

		if(isDone) {
			SceneManager.LoadScene(2);
		}

		isDone = true;

	}

	private void OnApplicationQuit() => Unsubscribe();
	private void OnDisable() => Unsubscribe();
	private void OnDestroy() => Unsubscribe();
	private void Unsubscribe() {
		EventSystem.onButtonPressed -= StartCountdown;
	}
}
