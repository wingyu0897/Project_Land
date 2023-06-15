using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
	public LandManager Instance;

	public float landSize = 15f;
	public Vector2Int position;
	public Land land;

	public Dictionary<Vector2Int, Land> lands = new Dictionary<Vector2Int, Land>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	public void AddLand(Vector2Int position)
	{
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
}
