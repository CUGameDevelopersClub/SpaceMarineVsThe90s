using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (LevelMaker))]
public class TerrainEditor : Editor {
	public override void OnInspectorGUI () {
		DrawDefaultInspector ();

		LevelMaker ld = (LevelMaker)target;

		if (GUILayout.Button ("Generate Level")) {
			ld.CreateLevel();
		}
	}
}
