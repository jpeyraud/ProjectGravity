using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Destroy (gameObject,5.0f);
		transform.Rotate(new Vector3(90.0f,90.0f,90.0f));	
	}

	// Update is called once per frame
	void Update () {
		
		
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Target")){
			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
}
