using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using System.IO;

public class chalo : MonoBehaviour {

	public float MoveSpeed = 10f;
	public float TurnSpeed = 2f;
	public float Gravity = 20f;
	public float Jump = 10f;

	float GravityEffect;
	float GroundDistance;
	Vector3 lookDir;
	Vector3 oldLookDir;

	CharacterController Player;

	void Start () {
		Player = GetComponent<CharacterController> ();
		GroundDistance = Player.bounds.extents.y;
	}

	void Update() {
		// check if we are jumping
		PlayerJump ();

		// get input
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
			
		// create our movement vector for player movement, and use it to set our look vector for rotation
		Vector3 movement = new Vector3 (h, Gravity, v) * MoveSpeed * Time.deltaTime;
		lookDir = new Vector3 (movement.x, 0f, movement.z);
		
		// determine method of rotation
		if (h != 0 || v != 0) {

			// create a smooth direction to look at using Slerp()
			Vector3 smoothDir = Vector3.Slerp(transform.forward, lookDir, TurnSpeed * Time.deltaTime);

			transform.rotation = Quaternion.LookRotation (smoothDir);
				
			// store the current smooth direction to use when the player is not providing input, providing consistency
			oldLookDir = smoothDir;
		} 
		else 
		{
			transform.rotation = Quaternion.LookRotation(oldLookDir);
		}

		// move the player using its CharacterController.Move method
		Player.Move (movement);

		// Apply our gravity to the player
		ApplyGravity ();
	}

	void PlayerJump() {
		if (IsGrounded ()) {
			if (Input.GetButtonDown ("Jump")) {
				GravityEffect -= Jump * Time.deltaTime;
				Debug.Log ("jump");
			}
		}
	}

	void ApplyGravity() {
		if (!IsGrounded ()) {
			GravityEffect -= Gravity * Time.deltaTime;
		} else {
			GravityEffect = 0f;
		}
	}

	bool IsGrounded() {
		return Physics.Raycast (transform.position, -Vector3.up, GroundDistance + 0.1f);
	}
}
