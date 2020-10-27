using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
	public float TimeLeft = 1;
	private float ResetTime = 0;
	private bool isTimerOn = false;
	private bool isDone = false;

	private void Awake() {
		EventSystem.onButtonPressed += StartCountdown;
		ResetTime = TimeLeft;
	}

	public void StartCountdown(char _) {
		if(!isTimerOn) {
			isTimerOn = true;
			StartCoroutine(wait());
		}	
	}

	IEnumerator wait() {
		while(TimeLeft > 0) {
			TimeLeft -= Time.deltaTime;
			yield return null;
		}
		isTimerOn = false;
		TimeLeft = ResetTime;

		EventSystem.onClearKeyboard();
		EventSystem.onNextString();
		EventSystem.onSwtichInputMethod();
		EventSystem.onSetPos();

		if (isDone)
        {
			SceneManager.LoadScene(0);
        }

		isDone = true;
		
	}
}
