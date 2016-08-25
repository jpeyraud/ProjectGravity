using UnityEngine;
using System.Collections;

public class MouseKeyboardControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		Vector3 newPos = gameObject.transform.position;
		gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Mouse ScrollWheel"),Input.GetAxis("Vertical")));
		if (Input.GetMouseButton (1)) {
			gameObject.transform.Rotate (new Vector3 (-Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"), 0.0f));
		}
	}
}
