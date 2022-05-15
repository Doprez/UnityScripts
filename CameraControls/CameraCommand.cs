using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCommand : MonoBehaviour
{
	
	[Header("Player Info")]
	public Faction PlayerFaction;

	private Vector2 _mousePosition;
	private RaycastHit hit;    
	private Camera _mainCamera;

	private void Awake()
	{
		_mainCamera = Camera.main;
		PlayerFaction = GetComponent<Faction>();
	}

	public void Command(Vector2 mousePosition, InputAction action)
	{
		_mousePosition = mousePosition;
		TrackMousePosition();
		if (action.WasPressedThisFrame())
		{
			ExecuteCommand();
		}
		
	}

	private void ExecuteCommand()
	{
		PlayerFaction.GiveUnitCommand(hit);
	}

	private void TrackMousePosition()
	{
		Ray ray = _mainCamera.GetComponent<Camera>().ScreenPointToRay(_mousePosition);

		if (!Physics.Raycast(ray, out hit, Mathf.Infinity)) { return; }

		HandleMouseCursorImage();
	}

	private void HandleMouseCursorImage()
	{
		//change cursor image based on what the mouse is pointing at
	}

}
