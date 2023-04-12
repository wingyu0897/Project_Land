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

		float y = Input.GetAxisRaw("Jump");

		movement.SetMove(new Vector3(x, y, z));
	}
}
