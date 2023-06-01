using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
	public Interactable closestInteract;
	public Interactable latestInteract;

	[SerializeField] private float radius = 1f;
	[SerializeField] private LayerMask targetLayer;
	[SerializeField] private RectTransform keyUI;
	[SerializeField] private TextMeshProUGUI infoText;

	private PlayerActionData actionData;
	private Camera cam;

	private void Awake()
	{
		actionData = GetComponent<PlayerActionData>();
		cam = Camera.main;
	}

	private void Update()
	{
		ResourceFinding();
		if (actionData.isInteracting) return;
		InteractInput();
	}

	private void LateUpdate()
	{
		if (closestInteract != null)
		{
			keyUI.position = cam.WorldToScreenPoint(closestInteract.gameObject.transform.position);
		}
	}

	private void InteractInput()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (closestInteract == null) return;

			actionData.isInteracting = true;
			keyUI.gameObject.SetActive(false);
			closestInteract.OnInteract();
		}
	}

	private void ResourceFinding()
	{
		if (closestInteract != null)
		{
			if (Vector3.Distance(closestInteract.gameObject.transform.position, transform.position) > radius)
			{
				if (actionData.isInteracting)
				{
					latestInteract.StopInteract();
				}
				keyUI.gameObject.SetActive(false);
				closestInteract = null;
			}
			else
			{
				if (!actionData.isInteracting)
				{
					infoText.text = closestInteract.Name;
					keyUI.gameObject.SetActive(true);
				}
			}
		}

		if (!actionData.isActive)
			return;

		Collider[] objects = Physics.OverlapSphere(transform.position, radius, targetLayer);
		foreach (Collider obj in objects)
		{
			if (closestInteract == null || Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(closestInteract.gameObject.transform.position, transform.position))
			{
				closestInteract = obj.GetComponent<Interactable>();
				latestInteract = closestInteract;
			}
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (UnityEditor.Selection.activeGameObject == gameObject)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);
			Gizmos.color = Color.white;
		}
	}
#endif
}
