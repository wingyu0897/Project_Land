using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LandManager))]
public class LandManagerEditor : Editor
{
	public Vector2Int position;
	public Land land;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Create Land"))
		{
			(target as LandManager).AddLand((target as LandManager).position, (target as LandManager).land);
		}
	}
}
