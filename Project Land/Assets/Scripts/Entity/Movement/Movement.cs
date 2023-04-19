 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private CharacterController charController;
	private PlayerAnimator animator;

	[SerializeField] private float jumpForce = 3f;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float gravity = -9.81f;
	private float verticalVelocity;

	private Vector3 moveVelocity;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		animator = GetComponent<PlayerAnimator>();
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

		animator.SetSpeed(moveVelocity.magnitude);
	}

	public void SetMove(Vector3 dir)
	{
		dir.y = 0;
		moveVelocity = dir;
	}

	public void Jump()
	{
		if (charController.isGrounded)
		{
			verticalVelocity = jumpForce;
			animator.SetJumpTrigger(true);
		}
	}

	public void SetRotate(Vector3 dir)
	{
		dir.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.05f);
	}
}
