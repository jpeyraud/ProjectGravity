using UnityEngine;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour {

	private float currentDelta;
	private float SpawnTickTime = 1.0f;

	public GameObject SpawnableItem;

	public enum AxisType
	{
		XAxis,
		ZAxis
	}

	// Use this for initialization
	void Start () {
		currentDelta = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		currentDelta += Time.deltaTime;
		// check if the trigger button is down or up
		if (Input.GetButtonDown ("Fire1")) {
			if (currentDelta >= SpawnTickTime && SpawnableItem != null) {
				GameObject newItem = (GameObject)Instantiate (SpawnableItem, transform.position, transform.rotation);
				newItem.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward, ForceMode.Impulse);
				currentDelta = 0.0f;
			}
		}
	}
}
