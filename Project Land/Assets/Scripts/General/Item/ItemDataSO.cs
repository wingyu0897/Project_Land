using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DATA/Item")]
public class ItemDataSO : ScriptableObject
{
	public string itemName;
	public Sprite image;
	public int maxCapacity = 1;
	public Item prefab;
	public float disappearTime = 60f;
}
