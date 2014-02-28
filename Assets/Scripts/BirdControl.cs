using UnityEngine;
// using System;
using System.Collections;
using System.Collections.Generic;

public class BirdControl : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	private bool fly = false;
	private float lastFlap = 0;
	private float timeBetweenFlaps = 0.3f;
	private Animator animator;
	
	private float moveForce = 10f;			// Amount of force added to move the player left and right.
	private float maxXSpeed = 5f;			// The fastest the player can travel in the x axis.
	private float maxYSpeed = 100f;
	private float flyForce = 50f;			// Amount of force added when the player jumps.
	private float interactionDistance = 0.9f;
	private int grabbing = 0;
	private Transform grabbedObject = null;
	private Vector3 grabbingPosition = new Vector3(0.5f, 0, 0);
	private Transform oldNearest; // FIXME: Ugly name

	void Awake() {
		animator = this.GetComponent<Animator> ();
	}

	void Start () {
	}

	// Given a list of objects, find the closest one

	Transform FindNearestObject(List<Transform> objects){
		Transform nearest = null;
		float nearestDistance = Mathf.Infinity;

		foreach (Transform o in objects) {
			float distance = Vector3.Distance (o.position, transform.position);
		
			if (distance < nearestDistance) {
				nearestDistance = distance;
				nearest = o;
			}
		}

		return nearest;
	}

	// Find a list of objects within the distance from us

	List<Transform> FindObjectsWithinRange(float range){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
		List<Transform> objects = new List<Transform>();

		foreach (Collider2D collider in colliders) {
			objects.Add (collider.transform);
		}

		return objects;
	}
	
	// Update is called once per frame

	void Update () {
		// Toggle flying

		if (Input.GetButton("Up")) {
			fly = true;
		} else {
			fly = false;
		}

		// Remove highlight on old nearest object

		if(oldNearest != null){
			oldNearest.SendMessage("Highlight", false);
		}

		// Find objects that can be interacted with

		var closeObjects = FindObjectsWithinRange(2.0f);
		List<Transform> interactiveObjects = new List<Transform>();

		// Filter nearby objects that can be interacted with
		
		foreach(var closeObject in closeObjects){
			var props = closeObject.GetComponent<Prop>();
			if(props != null && (props.grabbable == true || props.edible == true)){
				interactiveObjects.Add (closeObject);
			}
		}
		
		// Of the interactive objects, find the closest one
		
		var nearest = FindNearestObject(interactiveObjects);

		if(nearest != null && Vector2.Distance (nearest.position, transform.position) < interactionDistance){
			nearest.SendMessage("Highlight", true);
			oldNearest = nearest;

			var props = nearest.GetComponent<Prop>();

			if(props != null){
				// Grab object
				
				if (Input.GetMouseButton(0)) {
					// Grab things
					
					if(grabbing == 0 && props.grabbable == true){
						grabbedObject = nearest;
						grabbing = 1;
					}
				}
			}
		}

		// Let go of a  grabbed object

		if(!Input.GetMouseButton(0)){
			if (grabbedObject) {
				grabbing = 3;
			}
		}

		// Update grabbed object position every frame

		if (grabbing == 2) {
			grabbedObject.transform.localPosition = grabbingPosition;
		}
	}

	void FixedUpdate () {
		// Attach / remove grabbed object as needed

		if (grabbedObject) {
			if(grabbing == 1){
				grabbedObject.transform.parent = transform;
				grabbedObject.rigidbody2D.isKinematic = true;
				grabbedObject.collider2D.enabled = false;
				grabbing = 2;
			}

			if(grabbing == 3){
				grabbedObject.transform.parent = null;
				grabbedObject.rigidbody2D.isKinematic = false;
				grabbedObject.collider2D.enabled = true;
				// grabbedObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				grabbedObject = null;
				grabbing = 0;
			}
		}

		// Cache the horizontal input.

		float h = Input.GetButton ("Right") ? 1.0f : Input.GetButton ("Left") ? -1.0f : 0.0f;

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...

		if(Mathf.Abs (h * rigidbody2D.velocity.x) < maxXSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		if(h > 0)
			animator.SetBool ("direction", false);
		if(h < 0)
			animator.SetBool ("direction", true);

		if (fly) {
			if(Time.time - lastFlap > timeBetweenFlaps || lastFlap == 0.0f){
				lastFlap = Time.time;
				rigidbody2D.AddForce(new Vector2(0f, flyForce));
			}
			GetComponent<Animator> ().enabled = true;
			// animation.Play ();
			
		} else {
			GetComponent<Animator> ().enabled = false;
			// animation.Stop();
		}
	}

	public void Flip () {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x = -theScale.x;
		// transform.localScale = theScale;
	}
}