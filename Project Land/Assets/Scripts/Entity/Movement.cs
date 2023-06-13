using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private CharacterController charController;

	// Properties // 하드코딩
	[SerializeField] private float speed = 5f;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	public void Move(Vector3 direction)
	{
		Vector3 move = direction.normalized * speed;
		move.y = 0;
		charController.Move(move * Time.deltaTime);
		SetRotate(direction, 0.01f);
	}

	public void SetRotate(Vector3 dir, float lerpSpeed = 0.15f)
	{
		dir.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), lerpSpeed);
	}
}
