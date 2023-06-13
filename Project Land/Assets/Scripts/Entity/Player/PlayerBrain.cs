using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerBrain : MonoBehaviour
{
	[HideInInspector]
	public PlayerMovement movement;
    [HideInInspector]
    public PlayerActionData actionData;
	[HideInInspector]
	public PlayerAnimator animator;
	[HideInInspector]
	public PlayerHealth health;

	private void Awake()
	{
		movement = GetComponent<PlayerMovement>();
		actionData = GetComponent<PlayerActionData>();
		animator = transform.Find("Visual").GetComponent<PlayerAnimator>();
		health = GetComponent<PlayerHealth>();

		health.OnDead += Dead;
	}

	private void Start()
	{
		animator.Init();
		health.Init();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			Init();
		}
	}

	public void Dead()
	{
		movement.StopMovement();
		animator.SetDeadBool(true);
		animator.SetDeadTrigger(true);
		SelectItem.Instance.Deselect();

		actionData.isDead = true;
	}

	[ContextMenu("Init")]
	public void Init()
	{
		if (!Application.isPlaying)
			return;
		
		movement.CharController.enabled = false;
		transform.SetPositionAndRotation(Define.Instance.spawnPosition, Quaternion.Euler(Vector3.zero));
		movement.CharController.enabled = true;
		health.Init();
		animator.SetDeadBool(false);
		actionData.isDead = false;
	}
}
