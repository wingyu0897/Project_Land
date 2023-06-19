using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
	[SerializeField] private Slider daySlider;
    [SerializeField] private float startDayTime;
    [SerializeField] private float endDayTime;
    [SerializeField] private float dayTimeMultiply;
	private float maxDayTime;
    private float currentDayTime = 0;
    public int currentDay = 1;

    private bool isWorking = false;

	public UnityEvent<int> OnDayChanged = null;

	public void Init()
	{
		maxDayTime = startDayTime;
        currentDayTime = 0;
        currentDay = 1;
	}

    public void SetDayTimer(bool toggle)
	{
        isWorking = toggle;
	}

	private void Update()
	{
		if (isWorking)
		{
			currentDayTime += Time.deltaTime;

			if (currentDayTime >= maxDayTime)
			{
				currentDayTime = 0;
				currentDay++;
				maxDayTime *= dayTimeMultiply;
				maxDayTime = Mathf.Clamp(maxDayTime, 0, endDayTime);

				OnDayChanged?.Invoke(currentDay);
			}

			daySlider.value = currentDayTime / maxDayTime;
		}
	}
}
