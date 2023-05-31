using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	protected PlayerActionData actionData;

	private void Start()
	{
		actionData = Define.Instance.player.GetComponent<PlayerActionData>();
	}

	public abstract string Name { get; set; }
	public abstract void OnInteract();
}
