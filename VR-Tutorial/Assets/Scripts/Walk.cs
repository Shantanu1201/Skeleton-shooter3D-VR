using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Walk : MonoBehaviour {
	public float speed = 8.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	//string t;
	//public float h=0.0f, v=0.0f;
	//public GameObject play;




	void Update() {
		/*t = LoadEncodedFile();
		Debug.Log(t);
		h = (t[0] - 53) * 3 / 40f;
		v = (t[1] - 53) * 3 / 40f;*/
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {



			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			/*{
				Vector3 forward = play.transform.forward * h;
				forward.y = 0f;
				Vector3 right = play.transform.right * v;
				transform.position += forward;
				transform.position += right;
			}*/

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;

		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}



	/*public string LoadEncodedFile()
	{
		Debug.Log (Application.persistentDataPath);
		string path = Application.persistentDataPath;
		string s = File.ReadAllText(path + "/" + "Move" + ".txt");
		return (s);
	}*/

}