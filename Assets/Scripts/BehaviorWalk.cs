using UnityEngine;
using System.Collections;

public class BehaviorWalk : MonoBehaviour {

	private float maxMoveSpeed = 1f;
	private float moveForce = 5f;
	private Animator animator;

	void Walk (float direction) {
		if(gameObject.rigidbody2D.velocity.magnitude < maxMoveSpeed){
			gameObject.rigidbody2D.AddForce(new Vector3(direction * moveForce, 0f, 0f));
		}

		animator.SetBool ("direction", direction > 0);
	}

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
