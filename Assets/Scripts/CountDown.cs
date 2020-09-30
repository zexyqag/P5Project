using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
	public float Time = 1;
	public UnityEvent Event;

	private void Start() {
		StartCoroutine(wait(Time));
	}

	IEnumerator wait(float time) {
		yield return new WaitForSeconds(time);
		Event.Invoke();
	}
}
