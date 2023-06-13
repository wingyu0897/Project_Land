using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public static Define Instance;

	public Camera mainCamera;
	public Transform player;
	public Transform inventoryTrm;
	public Vector3 spawnPosition;

	private void Awake()
	{
		Instance = this;
		
		mainCamera = Camera.main;
		player = GameObject.Find("Player").transform;
		inventoryTrm = GameObject.Find("Inventory").transform;
	}
}
