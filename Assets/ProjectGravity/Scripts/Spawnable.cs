using UnityEngine;
using System.Collections;

public class Spawnable : MonoBehaviour {

    public GameObject target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            gameObject.GetComponent<Rigidbody>().velocity *= 0.8f;
            gameObject.GetComponent<Rigidbody>().AddForce((target.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position).normalized, ForceMode.Impulse);

            //gameObject.GetComponent<Rigidbody>().velocity = target.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
        }
            
	}
}
