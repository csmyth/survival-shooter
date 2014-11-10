using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed = 6f;		// Controls how fast player is

	Vector3 movement;				// Stores movement to be applied to player
	Animator anim;					// Stores animator applied to player
	Rigidbody playerRigidbody;		// Stores rigidbody applied to player
	int floorMask; 					// Used to tell raycast to only hit floor
	float camRayLength = 100f;		// Length of ray cast from camera

	// Used to set up references, whether script is enabled or not
	void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}
}
