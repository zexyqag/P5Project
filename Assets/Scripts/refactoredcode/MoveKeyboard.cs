using UnityEngine;

public class MoveKeyboard : MonoBehaviour {
	#region Public fields
	public float speed, max, min;
	public bool bUP;
	public GameObject Keyboard;
	#endregion

	#region Private fields
	private float fAngle;
	#endregion

	private void Start() {
		fAngle = Keyboard.GetComponent<Transform>().eulerAngles.x;
	}

	private void OnTriggerStay(Collider other) {
		if(other.CompareTag("UpAndDown")) {
			Rotate();
		}
	}


	/// <summary>
	/// Rotate the attached keyboard
	/// </summary>
	void Rotate() {
		fAngle = Keyboard.GetComponent<Transform>().eulerAngles.x;

		if(bUP && fAngle < max) {
			fAngle += speed * Time.deltaTime;
			Keyboard.transform.eulerAngles = (new Vector3(fAngle, 0, 0));
		} else if(!bUP && fAngle < min) {
			fAngle += -speed * Time.deltaTime;
			Keyboard.transform.eulerAngles = (new Vector3(fAngle, 0, 0));
		}
	}
}
