using UnityEngine;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour {

	private float currentDelta;
	private float SpawnTickTime = 1.0f;
	public float powerFactor;

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
				if (powerFactor == 0) {
					powerFactor = 10;
				}
				GameObject newItem = (GameObject)Instantiate (SpawnableItem, transform.position, transform.rotation);
				Vector3 force = gameObject.transform.forward;
				force.Scale (new Vector3 (powerFactor, powerFactor, powerFactor));
				newItem.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
				currentDelta = 0.0f;
			}
		}
	}
}
