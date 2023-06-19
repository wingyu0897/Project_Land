using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReservedLand
{
	public Land land;
	public Vector2Int position;
}

public class LandManager : MonoBehaviour
{
	public LandManager Instance;

	public int landMax;
	public float landSize = 15f;
	public Vector2Int position;
	public Land land;

	public List<Land> spawnableLands = new List<Land>();
	public List<ReservedLand> reserveds = new List<ReservedLand>();
	private int maxWeight = 0;

	public Dictionary<Vector2Int, Land> lands = new Dictionary<Vector2Int, Land>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		foreach (Land land in spawnableLands)
		{
			maxWeight += land.GetWeight();
		}
	}

	public void AddLand(Vector2Int position)
	{
		if (lands.Count >= landMax-1)
			return;

		Land land = GetRandomLand();
		foreach (ReservedLand reserved in reserveds)
		{
			if (position == reserved.position)
			{
				land = reserved.land;
			}
		}

		AddLand(position, land);
	}

	public void AddLand(Vector2Int position, Land land)
	{
		if (lands.ContainsKey(position) || position == Vector2Int.zero)
		{
			NotificationManager.Instance.Notification(null, "Land Error", "This position already have land.");
			return;
		}

		Land newLand = PoolManager.Instance.Pop(land.gameObject.name) as Land;
		newLand.transform.position = new Vector3(position.x * landSize, 0, position.y * landSize);
		lands.Add(position, newLand);

		newLand.OnSpawned();
	}

	private Land GetRandomLand()
	{
		int curWeight = 0;
		int weight = Random.Range(0, maxWeight + 1);

		foreach (Land land in spawnableLands)
		{
			curWeight += land.GetWeight();

			if (weight <= curWeight)
			{
				return land;
			}
		}

		return land;
	}
}
