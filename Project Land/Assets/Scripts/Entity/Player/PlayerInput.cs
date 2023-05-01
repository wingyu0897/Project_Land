using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
	private PlayerActionData actionData;
	private PlayerInventory inventory;

	public event Action OnAttackClickAction = null;
	public event Action OnClickAction = null;
	public event Action OnStartAcquisitionAction = null;
	public event Action OnRollingAction = null;

	private void Awake()
	{
		movement = GetComponent<PlayerMovement>();
		actionData = GetComponent<PlayerActionData>();
		inventory = GetComponent<PlayerInventory>();
	}

	private void Update()
	{
		UpdateMoveInput();
		UpdateRollingInput();
		UpdateClickInput();
		UpdateStartAcquisitionInput();
		UpdateItemInput();
	}

	private void UpdateMoveInput()
	{
		if (!actionData.isActive) return;

		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		movement.SetMove(new Vector3(x, 0, z));
	}

	private void UpdateRollingInput()
	{
		if (actionData.isRolling || !actionData.isActive) return;

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			OnRollingAction?.Invoke();
			movement.Roll();
		}
	}

	private void UpdateItemInput()
	{
		if (!actionData.canChange) return;
		UpdateDropItemInput();
		UpdateHotbarInput();
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

	private void UpdateStartAcquisitionInput()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			OnStartAcquisitionAction?.Invoke();
		}
	}

	private void UpdateClickInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!actionData.isAttacking && actionData.isActive)
				OnAttackClickAction?.Invoke();
			OnClickAction?.Invoke();
		}
	}
}
