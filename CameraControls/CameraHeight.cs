using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the CameraHeight script handles the max height that a camera can reach and moving the camera on the y axis
public class CameraHeight : MonoBehaviour
{
	[Header("Character Input Values")]
	public float ScrollSpeed;
	public float MaxHeight;

	private CharacterController _controller;
	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}

	//Due to some Unity wierdness I have to do a check for if the value is negative or positive
	//otherwise the value that gets passed in is equal to 120 or -120 ¯\_(ツ)_/¯
	public void ChangeCameraHeight(float heightChange)
	{
		var position = new Vector3(0,0,0);

		switch (heightChange)
		{
			case > 0:
				position.y -= ScrollSpeed;
				break;
			case < 0:
				position.y += ScrollSpeed;
				break;
		}
		
		_controller.Move(transform.TransformDirection(position));

		if (_controller.transform.position.y >= MaxHeight)
		{
			var currentPosition = _controller.transform.position;
			currentPosition.y = MaxHeight;
			
			//in order to change the height I have to disable the character controller in order for changes to work
			_controller.enabled = false;
			_controller.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
			_controller.enabled = true;
		}
	}
}
