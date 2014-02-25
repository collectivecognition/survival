using UnityEngine;
using System.Collections;

public class EggWhole : MonoBehaviour {
	private float breakVelocity = 5;

	public Transform half;

	void OnCollisionEnter2D (Collision2D collision) {
		if(collision.relativeVelocity.magnitude > breakVelocity){
			Instantiate (half, transform.position + Random.insideUnitSphere, transform.rotation);
			Instantiate (half, transform.position + Random.insideUnitSphere, transform.rotation);
			Destroy (gameObject);
		}
	}
}
