using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
	public Camera PlayerCamera;
	private float _xRotation = 0f;

	[Header("Character Input Values")]
	public float XSensitivity = 30f;
	public float YSensitivity = 30f;

	private void Awake()
	{
		PlayerCamera = Camera.main;
	}

	public void Rotate(Vector2 input)
	{
		float mouseX = input.x;
		float mouseY = input.y;

		//Calculate camera rotation for looking up and down
		_xRotation -= (mouseY * Time.deltaTime) * YSensitivity;
		_xRotation = Mathf.Clamp(_xRotation, -80, 80);

		//apply these values to Camera object
		PlayerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

		//rotate player to look left and right
		transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * XSensitivity);
	}
}
