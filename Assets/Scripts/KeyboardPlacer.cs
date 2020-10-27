using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlacer : MonoBehaviour {
	public List<GameObject> keys;
	public float Size;
	public float Spacing;
	public GameObject SpaceBar, BackSpace;
	public Vector3 startPosition;
	public Transform keyboard;


	private void Start() {
		
	}

	void Scale() {
		Spacing = Spacing / 1000;
		Size = Size / 1000;

		foreach (GameObject gameObject in keys) {
			gameObject.SetActive(true);
			gameObject.transform.localScale = new Vector3(1, 1, 1);
			gameObject.transform.localScale *= Size;
		}

		SpaceBar.transform.localScale *= Size;
		BackSpace.transform.localScale *= Size;

		SpaceBar.SetActive(true);
		BackSpace.SetActive(true);
	}

	[ContextMenu("place")]
	public void Place() {

		Scale();

		for (int i = 0; i < 10; i++) // top row
		{
			Vector3 newPos = new Vector3(startPosition.x + ((Spacing + Size) * i), startPosition.y, startPosition.z);
			keys[i].transform.localPosition = newPos;
		}

		for(int i = 0; i < 9; i++) // middle row
		{
			Vector3 newPos = new Vector3(startPosition.x + ((Spacing + Size) * i) + 0.05f, startPosition.y - (Spacing + Size), startPosition.z);
			//Debug.Log(newPos);
			keys[i + 10].transform.localPosition = newPos;
		}

		for(int i = 0; i < 7; i++) // button row
		{
			Vector3 newPos = new Vector3(startPosition.x + ((Spacing + Size) * i) + 0.1f, startPosition.y - (Spacing + Size) * 2, startPosition.z);
			keys[i + 19].transform.localPosition = newPos;
		}

		SpaceBar.transform.localPosition = new Vector3(startPosition.x + (((Spacing + Size) * 7) + 0.1f)/2, startPosition.y - (Spacing + Size) * 3, startPosition.z);
		BackSpace.transform.localPosition = new Vector3(startPosition.x + ((Spacing + Size) * 8) + 0.05f, startPosition.y - (Spacing + Size) * 3 + 0.050f, startPosition.z);
	}

	private void OnDrawGizmos() {
		Vector3 pos1 = keyboard.position, pos2 = keyboard.position + (Vector3.right * (Spacing / 1000 + Size / 1000) * 9);
		Gizmos.DrawSphere(pos1, 0.05f);
		Gizmos.DrawSphere(pos2, 0.05f);
		Gizmos.DrawLine(pos1, pos2);
		//Gizmos.DrawSphere(startPosition + (Vector3.right * ((Spacing + size) * i), 0.01f);
	}

}
