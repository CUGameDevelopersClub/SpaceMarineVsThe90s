using UnityEngine;
using System.Collections.Generic;

public class PlatformBase {
	public Vector2 Pivot;		
	public int Width;

	public RopeBase[] Ropes;
	public PlatformObjectData[] PlatformObjects;

	public PlatformBase (Vector2 pivot, int platformWidth) {
		Pivot = pivot;
		Width = platformWidth;
	}
}

public class RopeBase {
	public int PlatformPosition;
	public int Length;

	public RopeBase (int platformPosition, int length) {
		PlatformPosition = platformPosition;
		Length = length;
	}
}