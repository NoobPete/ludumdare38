using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DeckScript))]
public class DeckScriptEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		DeckScript myScript = (DeckScript)target;
		if (GUILayout.Button("Add new basic card"))
		{
			myScript.AddBasicCardToPile();
		}

		if (GUILayout.Button("Scramble"))
		{
			myScript.ScrambleCards();
		}
	}
}