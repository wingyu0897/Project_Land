using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
	private PlayerActionData actionData;
	private SelectItem selectManager;
	private Inventory inventory;

	public event Action OnAttackClickAction = null;
	public event Action OnClickAction = null;
	public event Action OnInteractAction = null;
	public event Action OnRollingAction = null;

	private void Awake()
	{
		movement = GetComponent<PlayerMovement>();
		actionData = GetComponent<PlayerActionData>();
		selectManager = GetComponent<SelectItem>();
		inventory = GetComponent<Inventory>();
	}

	private void Update()
	{
		if (actionData.isDead) return;

		UpdateMoveInput();
		UpdateRollingInput();
		UpdateClickInput();
		UpdateInteractInput();
		UpdateItemInput();
		UpdateInventoryInput();
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
		if (actionData.isRolling || actionData.isInteracting) return;

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
			selectManager.DropSelected(1);
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
				selectManager.Select(input[0] - '1');
			}
		}
	}

	private void UpdateInventoryInput()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			inventory.ShowInventory(!inventory.IsShowing);
		}
	}

	private void UpdateInteractInput()
	{
		if (actionData.isRolling) return;

		if (Input.GetKey(KeyCode.F))
		{
			OnInteractAction?.Invoke();
		}
	}

	private void UpdateClickInput()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (!actionData.isAttacking && actionData.isActive)
				OnAttackClickAction?.Invoke();
			OnClickAction?.Invoke();
		}
	}
}
