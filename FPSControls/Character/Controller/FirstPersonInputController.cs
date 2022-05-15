using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterGravity))]
[RequireComponent(typeof(CharacterRotation))]
[RequireComponent(typeof(CharacterInteraction))]
public class FirstPersonInputController : MonoBehaviour
{
    private FPSInput _input;
    private FPSInput.FirsPersonControlMapActions _firstPersonActions;

	private CharacterMover _characterMover;
	private CharacterRotation _cameraRotation;
	private CharacterGravity _characterGravity;
	private CharacterInteraction _characterInteraction;

	private void Awake()
	{
		_input = new FPSInput();
		_firstPersonActions = _input.FirsPersonControlMap;

		_characterMover = GetComponent<CharacterMover>();
		_cameraRotation = GetComponent<CharacterRotation>();
		_characterGravity = GetComponent<CharacterGravity>();
		_characterInteraction = GetComponent<CharacterInteraction>();

		_firstPersonActions.Sprint.started += x => _characterMover.Sprint(true);
		_firstPersonActions.Sprint.canceled += x => _characterMover.Sprint(false);
		_firstPersonActions.Jump.started += x => _characterGravity.Jump();

	}

	private void Update()
	{
		_characterMover.Move(_firstPersonActions.Move.ReadValue<Vector2>());
		_cameraRotation.Rotate(_firstPersonActions.Look.ReadValue<Vector2>());
	}

	private void OnEnable()
	{
		_firstPersonActions.Enable();
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void OnDisable()
	{
		
		_firstPersonActions.Disable();
		Cursor.lockState = CursorLockMode.None;
	}

}
