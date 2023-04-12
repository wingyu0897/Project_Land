 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private CharacterController charController;

	[SerializeField] private float jumpForce = 3f;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float gravity = -9.81f;
	private float verticalVelocity;

	private Vector3 moveVelocity;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		if (charController.isGrounded == false)
		{
			verticalVelocity += gravity * Time.fixedDeltaTime;
		}

		moveVelocity.Normalize();
		moveVelocity = Quaternion.Euler(0, 45, 0) * moveVelocity;
		moveVelocity *= Time.fixedDeltaTime * speed;

		Vector3 move = moveVelocity + verticalVelocity * Vector3.up;

		charController.Move(move);
	}

	public void SetMove(Vector3 dir)
	{
		if (charController.isGrounded)
		{
			verticalVelocity = dir.y * jumpForce;
		}

		dir.y = 0;
		moveVelocity = dir;
	}
}
