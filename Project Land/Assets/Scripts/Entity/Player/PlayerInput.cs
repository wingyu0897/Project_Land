using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
	private PlayerActionData actionData;

	private void Awake()
	{
		movement = GetComponent<PlayerMovement>();
		actionData = GetComponent<PlayerActionData>();
	}

	private void Update()
	{
		UpdateRollingInput();
		UpdateJumpInput();
		UpdateMoveInput();
	}

	private void UpdateMoveInput()
	{
		if (actionData.isRolling) return;

		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		movement.SetMove(new Vector3(x, 0, z));
	}

	private void UpdateJumpInput()
	{
		if (actionData.isRolling) return;

		if (Input.GetKey(KeyCode.Space))
			movement.Jump();
	}

	private void UpdateRollingInput()
	{
		if (actionData.isRolling) return;

		if (Input.GetKeyDown(KeyCode.LeftShift))
			movement.Roll();
	}
}
