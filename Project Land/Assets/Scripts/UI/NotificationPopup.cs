using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopup : PoolableMono
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI infoText;

	public void SetData(Sprite sprite, string title, string info)
	{
		image.sprite = sprite;
		titleText.text = title;
		infoText.text = info;
	}

	public override void Init()
	{
		image.sprite = null;
		titleText.text = "";
		infoText.text = "";
	}
}
