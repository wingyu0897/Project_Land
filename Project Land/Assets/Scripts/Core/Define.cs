using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public static Define Instance;

	public Transform player;
	public Transform inventoryTrm;

	private void Awake()
	{
		Instance = this;
		
		player = GameObject.Find("Player").transform;
		inventoryTrm = GameObject.Find("Inventory").transform;
	}
}
