using UnityEngine;
using System.Collections;

public class BirdControl : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	private bool fly = false;
	private float lastFlap = 0;
	private float timeBetweenFlaps = 0.3f;
	
	
	public float moveForce = 100.0f;			// Amount of force added to move the player left and right.
	private float maxXSpeed = 5f;			// The fastest the player can travel in the x axis.
	private float maxYSpeed = 500f;
	private float flyForce = 550f;			// Amount of force added when the player jumps.
	private float grabbableDistance = 0.9f;
	private int grabbing = 0;
	private Transform grabbedObject = null;
	private Vector3 grabbingPosition = new Vector3(0.5f, 0, 0);

	void Awake() {
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Flight controls

		if (Input.GetButton("Up")) {
			fly = true;
		} else {
			fly = false;
		}

		if (Input.GetButtonDown("Grab")) {
			// Grab things

			if(grabbing == 0){
				var nearestDistance = Mathf.Infinity;
				var grabbables = GameObject.FindGameObjectsWithTag ("Grabbable");
				Vector3 grabPosition = transform.position + grabbingPosition;
				Transform nearest = null;

				foreach (GameObject obj in grabbables) {
					var objectPosition = obj.transform.position;
					var distance = (objectPosition - grabPosition).sqrMagnitude;

					if (distance < nearestDistance) {
						nearest = obj.transform;
						nearestDistance = distance;
					}
				}

				if (nearest) {
					if (Vector2.Distance (nearest.position, transform.position) < grabbableDistance) {
						grabbedObject = nearest;
						grabbing = 1;
					}
				}
			}else{
				// Let go

				if (grabbedObject || (grabbing == 2 && Vector2.Distance (grabbedObject.position, transform.position) > grabbableDistance)) {
					grabbing = 3;
				}
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
				grabbedObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				grabbedObject = null;
				grabbing = 0;
			}
		}

		// Cache the horizontal input.

		float h = Input.GetButton ("Right") ? 1.0f : Input.GetButton ("Left") ? -1.0f : 0.0f;

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...

		if(h * rigidbody2D.velocity.x < maxXSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// Snag the player's physics velocity

		float x = rigidbody2D.velocity.x;
		float y = rigidbody2D.velocity.y;;

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxXSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			x = Mathf.Sign (rigidbody2D.velocity.x) * maxXSpeed;

		// If the player's vertical velocity is greater than the maxSpeed...
		if (Mathf.Abs (rigidbody2D.velocity.y) > maxYSpeed)
			// ... set the player's velocity to the maxSpeed in the y axis.
			y = Mathf.Sign (rigidbody2D.velocity.y) * maxYSpeed;

		rigidbody2D.velocity = new Vector2 (x, y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		if (fly) {
			// rigidbody2D.gravityScale = 0.2f;
			if(Time.time - lastFlap > timeBetweenFlaps || lastFlap == 0.0f){
				lastFlap = Time.time;
				rigidbody2D.AddForce(new Vector2(0f, flyForce));
			}
			
		} else {
			// rigidbody2D.gravityScale = 1.0f;
		}
	}

	public void Flip () {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x = -theScale.x;
		transform.localScale = theScale;
	}
}