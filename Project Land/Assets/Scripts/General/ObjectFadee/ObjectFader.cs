using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed;
    public float fadeAmount;

    private float originalOpacity;

    private Material[] mats;
    public bool doFade = false;

	private void Start()
	{
		mats = GetComponent<Renderer>().materials;
		foreach (Material mat in mats)
		{
			originalOpacity = mat.color.a;
		}
	}

	private void Update()
	{
		if (doFade)
		{
			FadeNow();
		}
		else
		{
			ResetFade();
		}
	}

	private void FadeNow()
	{
		foreach (Material mat in mats)
		{
			Color currentColor = mat.color;
			Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
									Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed));
			mat.color = smoothColor;
		}
	}

	private void ResetFade()
	{
		foreach (Material mat in mats)
		{
			Color currentColor = mat.color;
			Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
									Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed));
			mat.color = smoothColor;
		}
	}
}
