using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI countText;

	public void SetItem(ItemDataSO data, int count = 0)
	{
		image.sprite = data.image;
		countText.text = count.ToString();
		countText.enabled = count > 1;
	}
}
