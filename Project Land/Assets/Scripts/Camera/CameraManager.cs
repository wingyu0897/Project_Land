using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;

	private void Update()
	{
		transform.position = new Vector3(target.position.x, transform.position.y, target.transform.position.z);
	}
}
