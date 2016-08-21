using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {

    public float SpawnTickTime = 1f;
    public float SpawnRatePerCent = 25.0f;
    public GameObject SpawnableItem = null;
    public GameObject Target = null;


    private float currentDelta = 0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        currentDelta += Time.deltaTime;

        if (currentDelta > SpawnTickTime) {
            currentDelta = 0f;
            if (Random.value * 100f >= SpawnRatePerCent && SpawnableItem != null) {
                GameObject newItem = (GameObject) Instantiate(SpawnableItem, transform.position, transform.rotation);
                //newItem.GetComponent<>().move(Target);
                //newItem.GetComponent<Rigidbody>().velocity = Target.GetComponent<Transform>().position - GetComponent<Transform>().position;
                //newItem.GetComponent<Rigidbody>().velocity*=10f;
                newItem.GetComponent<Spawnable>().target = Target;
            }
        } 
	}
}
