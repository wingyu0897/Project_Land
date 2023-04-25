using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
	private PlayerActionData actionData;
	private PlayerInventory inventory;

	public event Action OnAttackAction = null;
	public event Action OnRollingAction = null;

	private void Awake()
	{
		movement = GetComponent<PlayerMovement>();
		actionData = GetComponent<PlayerActionData>();
		inventory = GetComponent<PlayerInventory>();
	}

	private void Update()
	{
		UpdateRollingInput();
		UpdateMoveInput();
		UpdateDropItemInput();
		UpdateHotbarInput();
		UpdateAttackInput();
	}

	private void UpdateMoveInput()
	{
		if (actionData.isRolling || !actionData.isActive) return;

		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		movement.SetMove(new Vector3(x, 0, z));
	}

	private void UpdateRollingInput()
	{
		if (actionData.isRolling) return;

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			OnRollingAction?.Invoke();
			movement.Roll();
		}
	}

	private void UpdateDropItemInput()
	{
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			if (inventory.CurrentHoldSpace == null) return;
			inventory.DropItem(inventory.CurrentHoldSpace, false);
		}
	}

	private void UpdateHotbarInput()
	{
		if (Input.anyKeyDown)
		{
			char[] input = Input.inputString.ToCharArray();
			if (input.Length == 0) return;
			if (input[0] - '0' >= 1 && input[0] - '0' <= 8)
			{
				inventory.SelectItem(input[0] - '0');
			}
		}
	}

	private void UpdateAttackInput()
	{
		if (actionData.isAttacking) return;

		if (Input.GetMouseButtonDown(0))
		{
			OnAttackAction?.Invoke();
		}
	}
}
