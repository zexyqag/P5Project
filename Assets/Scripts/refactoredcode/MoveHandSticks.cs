using UnityEngine;

public class MoveHandSticks : MonoBehaviour {
	public GameObject Head, Hand;

	private void Update() {
		MoveThis();
		LookAtHand();
	}

	/// <summary>
	/// Move the gameObject to the position of the Head gameObject
	/// </summary>
	void MoveThis() {
		transform.position = Head.GetComponent<Transform>().transform.position;
	}

	/// <summary>
	/// Align the gameObject with the Hand gameObject 
	/// </summary>
	void LookAtHand() {
		transform.LookAt(Hand.GetComponent<Transform>().transform.position);
	}
}
