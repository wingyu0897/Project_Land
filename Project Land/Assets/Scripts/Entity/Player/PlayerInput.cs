using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Movement movement;

	private void Awake()
	{
		movement = GetComponent<Movement>();
	}

	private void Update()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		if (Input.GetKey(KeyCode.Space))
			movement.Jump();

		movement.SetMove(new Vector3(x, 0, z));
		if (x != 0 || z != 0)
			movement.SetRotate(Quaternion.Euler(0, 45, 0) * new Vector3(x, 0, z));
	}
}
