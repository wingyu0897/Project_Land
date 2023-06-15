using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private CharacterController charController;

	// Properties // 하드코딩
	[SerializeField] private float speed = 5f;
	[SerializeField] private float gravity = -9.81f;

	private float verticalVelocity;
	private Vector3 moveVelocity = Vector3.zero;
	private Vector3 inputVelocity = Vector3.zero;

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

		CalculateMoveVelocity();

		Vector3 move = moveVelocity + verticalVelocity * Vector3.up;
		charController.Move(move);
	}

	private void CalculateMoveVelocity()
	{
		inputVelocity.Normalize();
		moveVelocity = inputVelocity;
		moveVelocity *= Time.fixedDeltaTime * speed;
		if (moveVelocity.sqrMagnitude > 0)
		{
			SetRotate(moveVelocity);
		}
	}

	public void SetMove(Vector3 dir)
	{
		dir.y = 0;
		inputVelocity = dir;
		moveVelocity = dir;
	}

	public void StopMovement()
	{
		inputVelocity = Vector3.zero;
		moveVelocity = Vector3.zero;
	}

	public void SetRotate(Vector3 dir, float lerpSpeed = 0.15f)
	{
		dir.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), lerpSpeed);
	}
}
