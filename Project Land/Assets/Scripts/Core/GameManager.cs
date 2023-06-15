using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	private DayManager dayManager;
	private LandManager landManager;

	private int landExpand = 1;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		dayManager = GetComponent<DayManager>();
		landManager = transform.Find("LandManager").GetComponent<LandManager>();

		Init();
		StartGame();
	}

	public void Init()
	{
		landExpand = 1;

		dayManager.Init();
	}

	public void StartGame()
	{
		dayManager.SetDayTimer(true);
	}

	public void StopGame()
	{
		dayManager.SetDayTimer(false);
	}

	public void AddLands()
	{
		for (int i = -landExpand; i <= landExpand; ++i)
		{
			for (int j = -landExpand; j <= landExpand; ++j)
			{
				landManager.AddLand(new Vector2Int(j, i));
			}
		}

		landExpand++;
	}
}
