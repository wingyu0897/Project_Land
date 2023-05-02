 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController charController;
	private PlayerAnimator animator;
	private PlayerActionData actionData;

	// Properties // 하드코딩
	[SerializeField] private float speed = 5f;
	[SerializeField] private float gravity = -9.81f;

	private float verticalVelocity;
	private Vector3 moveVelocity;
	private Vector3 inputVelocity;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		animator = transform.Find("Visual").GetComponent<PlayerAnimator>();
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
		if (!actionData.isActive) return;

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

	public void StopMovement()
	{
		inputVelocity = Vector3.zero;
		moveVelocity = Vector3.zero;
	}

	public void Roll(Vector3 dir = default(Vector3))
	{
		if (!charController.isGrounded) return;

		actionData.isRolling = true;
		actionData.isActive = false;
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
		actionData.isActive = true;
	}

	public void SetRotate(Vector3 dir, float lerpSpeed = 0.15f)
	{
		dir.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), lerpSpeed);
	}

	public void SetPosition(Vector3 pos)
	{
		charController.Move(pos);
	}
}
