using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {
	private float timeSinceCreation;
	// Use this for initialization
	void Start () {
		timeSinceCreation = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		timeSinceCreation += Time.deltaTime;
		if (timeSinceCreation > 10.0f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Target")){
			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
}
