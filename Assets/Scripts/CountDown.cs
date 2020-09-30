using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
	public float TimeLeft = 1;
	public UnityEvent Event;

	public void StartCountdown() {
		StartCoroutine(wait());
	}

	IEnumerator wait() {
		while(TimeLeft > 0) {
			TimeLeft -= Time.deltaTime;
			yield return null;
		}

		Event.Invoke();
	}
}
