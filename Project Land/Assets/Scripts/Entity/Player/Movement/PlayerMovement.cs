 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController charController;
	public CharacterController CharController => charController;
	private PlayerAnimator animator;
	private PlayerActionData actionData;

	// Properties // 하드코딩
	[Header("Data")]
	[SerializeField] private MovementDataSO data;
	[SerializeField] private float gravity = -9.81f;

	[Header("Stamina")]
	[SerializeField] private float maxStamina;
	[SerializeField] private float curStamina;
	[SerializeField] private Slider staminaSlider;
	[SerializeField] private float staminaRecoverySpeed;
	[SerializeField] private float rollCost;

	// viriables
	private float verticalVelocity;
	private Vector3 moveVelocity = Vector3.zero;
	private Vector3 inputVelocity = Vector3.zero;
	private Vector3 walkDir = Vector3.zero;
	private bool rotateByVelocity = true;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		animator = transform.Find("Visual").GetComponent<PlayerAnimator>();
		actionData = GetComponent<PlayerActionData>();

		curStamina = maxStamina;
	}

	private void FixedUpdate()
	{
		if (actionData.isDead)
			return;

		curStamina += Time.fixedDeltaTime * staminaRecoverySpeed;
		curStamina = Mathf.Clamp(curStamina, 0, maxStamina);
		staminaSlider.value = curStamina / maxStamina;

		if (charController.isGrounded == false)
		{
			verticalVelocity += gravity * Time.fixedDeltaTime;
		}

		Vector3 forwardVec = Quaternion.Inverse(transform.rotation) * inputVelocity.normalized;
		if (forwardVec == Vector3.zero)
			walkDir = Vector3.zero;
		walkDir = Vector3.Lerp(walkDir, forwardVec, 0.1f);
		animator.SetMove(new Vector2(walkDir.x, walkDir.z).normalized);

		CalculateMoveVelocity();

		Vector3 move = moveVelocity + verticalVelocity * Vector3.up;
		charController.Move(move);
		animator.SetSpeed(moveVelocity.magnitude);


		if (moveVelocity.sqrMagnitude > 0 && rotateByVelocity)
		{
			SetRotate(moveVelocity);
		}
		else if (rotateByVelocity == false)
		{
			RaycastHit hit;
			Physics.Raycast(Define.Instance.mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 50f, 1 << LayerMask.NameToLayer("Ground"));
			if (hit.collider != null)
			{
				SetRotate(hit.point - transform.position, 1f);;
			}
		}
	}

	private void CalculateMoveVelocity()
	{
		if (!actionData.isActive) return;

		inputVelocity.Normalize();

		moveVelocity = Quaternion.Euler(0, 45, 0) * inputVelocity;
		if (actionData.canRun)
		{
			moveVelocity *= Time.fixedDeltaTime * data.runSpeed;
		}
		else
		{
			moveVelocity *= Time.fixedDeltaTime * data.walkSpeed;
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
		if (!charController.isGrounded || curStamina < rollCost) return;

		curStamina -= rollCost;

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
		bool rotV = rotateByVelocity;
		rotateByVelocity = true;

		yield return new WaitForSeconds(1f);

		actionData.isRolling = false;
		actionData.isActive = true;
		rotateByVelocity = rotV;
	}

	public void StopRolling()
	{
		actionData.isRolling = false;
		actionData.isActive = true;
		animator.Animator.Play("Idle");
	}

	public void SetRotate(Vector3 dir, float lerpSpeed = 0.15f)
	{
		if (!actionData.canRotate)
			return;

		dir.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), lerpSpeed);
	}

	public void SetPosition(Vector3 pos)
	{
		charController.Move(pos - transform.position);
	}

	public void ChangeMovementMode()
	{
		ChangeMovementMode(!rotateByVelocity);
	}

	public void ChangeMovementMode(bool run)
	{
		//actionData.canRun = run;
		rotateByVelocity = run;

		//animator.SetWalkTrigger(!actionData.canRun);
		//animator.SetWalkBool(!actionData.canRun);

		if (run == true)
			StopMovement();
	}
}
