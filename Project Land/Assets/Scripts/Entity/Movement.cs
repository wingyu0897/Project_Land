using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody rigid;

	// Properties // 하드코딩
	[SerializeField] private float speed = 5f;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 direction)
	{
		direction.y = 0;
		rigid.velocity = direction.normalized * speed;
		SetRotate(direction, 0.05f);
	}

	public void SetRotate(Vector3 dir, float lerpSpeed = 0.15f)
	{
		dir.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), lerpSpeed);
	}
}
