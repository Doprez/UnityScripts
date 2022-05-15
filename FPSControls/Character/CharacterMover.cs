using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{

    [Header("Character Input Values")]
    public float MoveSpeed = 5;
    public float SprintMultiplier = 2;
    public bool IsSprinting;

    private CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        //If player is holding sprint button and is on the ground only apply multiplier to forward and back directions
        if (IsSprinting)
        {
            moveDirection.z = input.y * SprintMultiplier;
            moveDirection.x = input.x * SprintMultiplier;
        }
        else
        {
            moveDirection.z = input.y;
            moveDirection.x = input.x;
        }

        //handle character forward and sideways motion
        _controller.Move(transform.TransformDirection(moveDirection) * MoveSpeed * Time.deltaTime);
    }

    public void Sprint(bool isSprinting)
    {
        IsSprinting = isSprinting;
    }

}
