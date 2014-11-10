using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;			// Controls how fast player is

	Vector3 movement;					// Stores movement to be applied to player
	Animator anim;						// Stores animator applied to player
	Rigidbody playerRigidbody;			// Stores rigidbody applied to player
	int floorMask; 						// Used to tell raycast to only hit floor
	float camRayLength = 100f;			// Length of ray cast from camera

	// Used to set up references, whether script is enabled or not
	void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}

	// Fires on each physics update
	void FixedUpdate() {
		// Note to self: An axis is input. Unity has some defaults, incuding "Horizontal" and "Vertical"
		float h = Input.GetAxisRaw ("Horizontal");			// Raw so that values will only be -1, 0, 1. Therefore, no gradual increase to full speed
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	// Note to self: Move, Turning, and Animating are separate functions to increase modularity

	void Move(float h, float v) {
		movement.Set (h, 0f, v);			// x and z components are along the ground. We don't want the player to fly, so no y component

		// Normalize to prevent advantage for moving diagonally
		// Multiply by our set speed so we're not always moving at speed = 1
		// Use deltaTime to prevent FixedUpdate() from moving the player every 50th of a second (when FixedUpdate() is called)
		movement = movement.normalized * speed * Time.deltaTime;

		// Apply input movement to player character
		// Move relative to current position with addition
		playerRigidbody.MovePosition (transform.position + movement);
	}

	// No parameters required because direction facing is based on mouse input, not keyboard input stored in movement class var
	// We want the character to turn to face the same point the camera is looking at
	void Turning() {
		// ScreenPointToRay = Ray from mouse position on computer screen 
		// camRay is ray from mouse position cast into scene
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;			// Gather information from camRay about where the floor quad is hit

		// Action of casting ray
		// Use if to prevent use when no hit on floor quad
		// out = We want information out of this function and to store it in the given var
		// We only want to try to hit the floorMask
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;			// We don't want the character to lean back

			// Can't set rotation based on vector, so need to convert to apply
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse); 
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	// Requires parameters because walking or idle depends on input
	void Animating(float h, float v) {
		bool walking = h != 0f || v != 0f;			// We're walking if there's any input in any direction
		anim.SetBool ("IsWalking", walking);
	}
}
