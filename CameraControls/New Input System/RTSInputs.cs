using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//The RTSInputs script handles making the calls to the other scripts that make actual changes for the player
//IE movement and camera rotation
public class RTSInputs : MonoBehaviour
{
	private RTSInput _rtsInput;
	private RTSInput.RTSActions RTSControls;
	private CameraMovement _movement;
	private CameraRotation _rotation;
	private CameraHeight _height;
	private CameraSelection _selection;
	private CameraCommand _command;
    
	private void Awake()
	{
		//getting needed scripts for this to work causes quite a few dependencies but I like the separation
		_rtsInput = new RTSInput();
		_movement = GetComponent<CameraMovement>();
		_rotation = GetComponent<CameraRotation>();
		_height = GetComponent<CameraHeight>();
		_selection = GetComponent<CameraSelection>();
		_command = GetComponent<CameraCommand>();
		
		RTSControls = _rtsInput.RTS;
		//subscribing events
		RTSControls.Sprint.started += ctx => _movement.Sprint(true);
		RTSControls.Sprint.canceled += ctx => _movement.Sprint(false);
		RTSControls.LockRotation.started += ctx => _rotation.LockRotation(true);
		RTSControls.LockRotation.canceled += ctx => _rotation.LockRotation(false);
		RTSControls.Height.started += ctx => _height.ChangeCameraHeight(RTSControls.Height.ReadValue<float>());
	}

	private void OnEnable()
	{
		RTSControls.Enable();
	}

	private void OnDisable()
	{
		RTSControls.Disable();
	}

	private void Update()
	{
		_selection.Selection(Mouse.current.position.ReadValue(), RTSControls.Select);
		_command.Command(Mouse.current.position.ReadValue(), RTSControls.Command);
	}

	private void FixedUpdate()
	{
		_movement.Move(RTSControls.Move.ReadValue<Vector2>());
	}

	private void LateUpdate()
	{
		_rotation.ProcessLook(RTSControls.Look.ReadValue<Vector2>());
	}

}