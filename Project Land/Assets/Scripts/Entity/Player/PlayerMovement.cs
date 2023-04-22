 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController charController;
	private PlayerAnimator animator;
	private PlayerActionData actionData;

	// Properties // 하드코딩
	[SerializeField] private float jumpForce = 3f;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float gravity = -9.81f;

	private float verticalVelocity;
	private Vector3 moveVelocity;
	private Vector3 inputVelocity;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		animator = GetComponent<PlayerAnimator>();
		actionData = GetComponent<PlayerActionData>();
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
		animator.SetSpeed(moveVelocity.magnitude);
	}

	private void CalculateMoveVelocity()
	{
		if (actionData.isRolling) return;

		inputVelocity.Normalize();
		moveVelocity = Quaternion.Euler(0, 45, 0) * inputVelocity;
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

	public void Jump()
	{
		if (charController.isGrounded)
		{
			verticalVelocity = jumpForce;
			animator.SetJumpTrigger(true);
		}
	}

	public void Roll(Vector3 dir = default(Vector3))
	{
		if (!charController.isGrounded) return;

		actionData.isRolling = true;
		animator.SetRollTrigger();

		if (dir == default(Vector3))
		{
			dir = Vector3.zero;
		}

		SetMove(transform.forward * 0.15f);
		StartCoroutine(Rolling());
	}

	private IEnumerator Rolling()
	{
		yield return new WaitForSeconds(1f);

		actionData.isRolling = false;
	}

	public void SetRotate(Vector3 dir)
	{
		dir.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.15f);
	}
}
