//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ResourceFinder : MonoBehaviour
//{
//	public Resource closestResource;
//	public Resource currentResource;
//	private int resourceLayer;
//	[SerializeField] private float radius = 1f;
//	[SerializeField] private RectTransform resourceUI;

//	private bool finding = true;

//	private PlayerInput input;
//	private PlayerActionData actionData;
//	private Camera cam;

//	public event Action OnStartObtaining = null;
//	public event Action OnStopObtaining = null;

//	private void Awake()
//	{
//		input = GetComponent<PlayerInput>();
//		actionData = GetComponent<PlayerActionData>();
//		cam = Camera.main;
//		resourceLayer = LayerMask.NameToLayer("Resource");
//	}

//	private void Start()
//	{
//		input.OnStartAcquisitionAction += StartHandle;
//	}

//	private void Update()
//	{
//		ResourceFinding();
//	}

//	private void ResourceFinding()
//	{
//		if (!finding || !actionData.isActive)
//		{
//			resourceUI.gameObject.SetActive(false);
//			return;
//		}

//		Collider[] resources = Physics.OverlapSphere(transform.position, radius, 1 << resourceLayer);
//		foreach (Collider res in resources)
//		{
//			if (closestResource == null || Vector3.Distance(res.transform.position, transform.position) < Vector3.Distance(closestResource.transform.position, transform.position))
//			{
//				closestResource = res.GetComponent<Resource>();
//			}
//		}
//		if (closestResource != null)
//		{
//			if (Vector3.Distance(closestResource.transform.position, transform.position) > radius)
//			{
//				closestResource = null;
//				resourceUI.gameObject.SetActive(false);
//			}
//			else
//			{
//				if (closestResource.Obtainable())
//				{
//					resourceUI.gameObject.SetActive(true);
//					resourceUI.position = cam.WorldToScreenPoint(closestResource.transform.position);
//				}
//				else
//				{
//					resourceUI.gameObject.SetActive(false);
//				}
//			}
//		}
//	}

//	private void StartHandle()
//	{
//		if (currentResource == null)
//		{
//			if (closestResource?.Obtainable() == false || actionData.isObtaining)
//				return;

//			if (closestResource != null)
//			{
//				SelectItem.Instance.OnDeselectItem.AddListener(StopObtaining);
//				Define.Instance.player.GetComponent<PlayerMovement>().StopMovement();
//				finding = false;
//				actionData.isObtaining = true;
//				closestResource.OnStartObtain();
//				currentResource = closestResource;
//				OnStartObtaining?.Invoke();
//			}
//		}
//		else
//		{
//			StopObtaining();
//		}
//	}

//	public void StopObtaining()
//	{
//		SelectItem.Instance.OnDeselectItem.RemoveListener(StopObtaining);
//		finding = true;
//		actionData.isObtaining = false;
//		currentResource.OnStopObtain();
//		currentResource = null;
//		OnStopObtaining?.Invoke();
//	}

//#if UNITY_EDITOR
//	private void OnDrawGizmos()
//	{
//		if (UnityEditor.Selection.activeGameObject == gameObject)
//		{
//			Gizmos.color = Color.red;
//			Gizmos.DrawWireSphere(transform.position, radius);
//			Gizmos.color = Color.white;
//		}
//	}
//#endif
//}
