using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;				// Controls how fast player is

	Vector3 movement;						// Stores movement to be applied to player
	Animator anim;							// Stores animator applied to player
	Rigidbody playerRigidbody;				// Stores rigidbody applied to player
	int floorMask; 							// Used to tell raycast to only hit floor
	float camRayLength = 100f;				// Length of ray cast from camera

	// Used to set up references, whether script is enabled or not
	void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}

	// Fires on each physics update
	void FixedUpdate() {
		// Note to self: An axis is input. Unity has some defaults, incuding "Horizontal" and "Vertical"
		float h = Input.GetAxisRaw ("Horizontal"); 		// Raw so that values will only be -1, 0, 1. Therefore, no gradual increase to full speed
		float v = Input.GetAxisRaw ("Vertical");

	}

	// Note to self: Move, Turn, and Animation are separate functions to increase modularity

	void Move(float h, float v) {
		movement.Set (h, 0f, v); 			// x and z components are along the ground. We don't want the player to fly, so no y component

		// Normalize to prevent advantage for moving diagonally
		// Multiply by our set speed so we're not always moving at speed = 1
		// Use deltaTime to prevent FixedUpdate() from moving the player every 50th of a second (when FixedUpdate() is called)
		movement = movement.normalized * speed * Time.deltaTime;

		// Apply input movement to player character
		// Move relative to current position with addition
		playerRigidbody.MovePosition (transform.position + movement);
	}
}
