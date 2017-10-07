using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Level Data", menuName = "Level/Create New Level")]
public class LevelPrototype : ScriptableObject {
	public int PlatformCount = 300;
	public float MinDistance = 20;
	public int MapRadius = 250;

	public float PerlinScale = 100;
	public float PerlinThreshold = 0.5f;

	public PlatformObjectData[] PlatformObjects;
}
