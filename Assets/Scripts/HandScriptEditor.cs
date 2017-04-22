using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HandScript))]
public class HandScriptEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		HandScript myScript = (HandScript)target;
		if (GUILayout.Button("Draw card"))
		{
			myScript.AddCardToHand(DeckScript.main.TakeTopCard());
		}

		if (GUILayout.Button("Nope"))
		{
		}
	}
}