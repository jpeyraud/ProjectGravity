using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Destroy (gameObject,5.0f);
		transform.Rotate (new Vector3 (90.0f, 90.0f, 90.0f));
	}

	// Update is called once per frame
	void Update () {
		
		
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Target")){
			collider.gameObject.AddComponent<TriangleExplosion>();
			StartCoroutine(collider.gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
			collider.gameObject.GetComponent<Rigidbody> ().AddExplosionForce (10.0f, collider.gameObject.transform.position, 5.0f);
			Destroy (gameObject);
		}
	}
}
