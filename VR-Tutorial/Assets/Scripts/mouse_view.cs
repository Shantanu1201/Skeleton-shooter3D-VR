using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_view : MonoBehaviour {
	float yRotation;
	float xRotation;
	float lookSensitivity = 4;
	float currentXRotation;
	float currentYRotation;
	float xRotationV;
	float yRotationV;
	float lookSmoothnes = .1f;

	void Update ()
	{
		yRotation += Input.GetAxis ("Mouse X") * lookSensitivity;
		xRotation -= Input.GetAxis ("Mouse Y") * lookSensitivity;
		xRotation = Mathf.Clamp (xRotation, -80, 90);
		currentXRotation = Mathf.SmoothDamp (currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
		currentYRotation = Mathf.SmoothDamp (currentYRotation, yRotation, ref yRotationV, lookSmoothnes);
		transform.rotation = Quaternion.Euler (xRotation, yRotation, 0);
	}
}
