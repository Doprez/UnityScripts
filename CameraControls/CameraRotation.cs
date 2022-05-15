using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the CameraRotation script handles rotating the camera if the unlock rotation button is pressed
public class CameraRotation : MonoBehaviour
{
	
	public Camera PlayerCamera;
	private float _xRotation = 0f;
	private bool IsRotationLocked;
	
	[Header("Character Input Values")]
	public float XSensitivity = 30f;
	public float YSensitivity = 30f;
	public float BottomClamp = -80;
	public float TopClamp = 80;

	private void Awake()
	{
		PlayerCamera = Camera.main;
	}

	public void ProcessLook(Vector2 input)
	{
		if (IsRotationLocked)
		{
			float mouseX = input.x;
			float mouseY = input.y;

			//Calculate camera rotation for looking up and down
			_xRotation -= (mouseY * Time.deltaTime) * YSensitivity;
			_xRotation = Mathf.Clamp(_xRotation, BottomClamp, TopClamp);

			//apply these values to Camera object
			PlayerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

			//rotate player to look left and right
			transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * XSensitivity);
		}
	}

	public void LockRotation(bool isRotationLocked)
	{
		IsRotationLocked = isRotationLocked;
	}
}
