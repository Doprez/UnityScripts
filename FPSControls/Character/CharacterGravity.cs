using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : MonoBehaviour
{

	private CharacterController _characterController;

	public float Gravity = 9.8f;
	public float jumpSpeed = 8.0F;


	private Vector3 moveDirection = Vector3.zero;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
	}

	void Update()
    {
		moveDirection.y -= Gravity * Time.deltaTime;
		_characterController.Move(moveDirection * Time.deltaTime);
	}

	public void Jump()
	{
		if (_characterController.isGrounded)
		{
			moveDirection.y = jumpSpeed;
		}
	}

}
