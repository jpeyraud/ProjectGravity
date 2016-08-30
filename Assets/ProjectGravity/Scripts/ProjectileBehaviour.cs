using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Destroy (gameObject,5.0f);
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
