using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
	public float TimeLeft = 1;
	private float RestTime = 0;

	private void Awake() {
		RestTime = TimeLeft;
	}

	public void StartCountdown() {
		StartCoroutine(wait());
	}

	public void ResetCountDown() {
		TimeLeft = RestTime;
	}

	IEnumerator wait() {
		while(TimeLeft > 0) {
			TimeLeft -= Time.deltaTime;
			yield return null;
		}

		EventSystem.onSwtichInputMethod();
	}
}
