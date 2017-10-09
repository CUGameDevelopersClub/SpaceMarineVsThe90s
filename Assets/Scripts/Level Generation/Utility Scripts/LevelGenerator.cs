using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGenerator {
	public static void GenerateLevel(LevelPrototype levelData) {
		/*Summary of the platform generation:
		 * This function is static, do not make objects of this class, only call it. 
		 * 
		 * How this code works:
		 * 1. A list is declared to store all the platforms that are spawned
		 * 2. A 2 dimensional int array is declared to store each tile position (this is important for rope spawning)
		 * 3. Enter a while loop that will execute until the length of the platforms list is > our defined platform max count,
		 * 		or the code exceeds a max iteration count
		 * 4. A random point is generated within the size of the map
		 * 5. The code now checks if the random point is valid against the perlin noise field (if the value of the nosie field
		 * 		at that point is greater than the PerlinThreshold (defined in LevelData), then the code will continue)
		 * 6. The random width of the platform is generated
		 * 7. The random position of the platform is tested against all other platforms that exist, if it is too close, the point
		 * 		is thrown away, and the while loop will start over
		 * 8. If the platform has passed all the previous tests, it will now be added to the list of platforms 
		 * 9. The appropriate points in the 2D int array are set to 1 to store the platform tiles
		 * 10. The ropes are generated (see summary below), and this class returns an array of platforms
		*/

		LevelPrototype ld = levelData;
		List <PlatformBase> platforms = new List<PlatformBase> (); 
		int[,] levelArray = new int[ld.MapRadius, ld.MapRadius];

		int iterationCount = 0;	//prevent an infinite loop

		while (platforms.Count < ld.PlatformCount) {
			if (iterationCount >= ld.PlatformCount * 100) {
			//	Debug.Log ("Stopping platform generation at " + platforms.Count);

				break;
			}

			Vector2 randomPoint = new Vector2 (Random.Range (0, ld.MapRadius/2), Random.Range (0, ld.MapRadius));

			if (Mathf.PerlinNoise (randomPoint.x / ld.PerlinScale, randomPoint.y / ld.PerlinScale) < ld.PerlinThreshold)
				continue;

			int platformWidth = Random.Range (5, 40);


			bool tooClose = false;
			foreach (PlatformBase pb in platforms) {
				if (Vector2.Distance (pb.Pivot + Vector2.right * platformWidth / 2f, randomPoint) < ld.MinDistance || Mathf.Abs(randomPoint.y - pb.Pivot.y) < 3) {
					tooClose = true;
					break;
				}
			}

			if (!tooClose) {
				PlatformBase pb = new PlatformBase (randomPoint, platformWidth);

				#region Temporary Gate spawning stuff
				List <PlatformObjectData> po = new List <PlatformObjectData> ();

				//make start gate
				if (platforms.Count == 0) {
					PlatformObjectData startGate = levelData.PlatformObjects [0];
					startGate.Object.transform.position = new Vector2 (pb.Pivot.x + Random.Range (1, platformWidth) + 0.5f, pb.Pivot.y + 1.5f);
					Level.StartGate = startGate;
				}

				//make end gate
				if (platforms.Count == 1) {
					PlatformObjectData endGate = levelData.PlatformObjects [1];
					endGate.Object.transform.position = new Vector2 (pb.Pivot.x + Random.Range (1, platformWidth) + 0.5f, pb.Pivot.y + 1.5f);
					Level.EndGate = endGate;
				}

				pb.PlatformObjects = po.ToArray ();
				#endregion

				platforms.Add (pb);

				for (int i = 0; i < pb.Width; i++) {
					levelArray [(int)pb.Pivot.x + i, (int)pb.Pivot.y] = 1;
				}
			}

			iterationCount++;
		}

		Level.platforms = GenerateRopes (platforms.ToArray (), levelArray);
	}

	/*Summary of the rope generation:
	 * 1. We get the array of platforms, and our 2D int array that stores our platform tiles
	 * 2. We now iterate through each platform to make a rope
	 * 3. We then iterate through the 2D array to check for an intersection with a platform
	 * 4. If there is no intersection, or the vertical test position is outside the array, 
	 * 		then the length of the rope is maxRopeLength, and the rope is added to the array of ropes
	 * 5. If there is an intersection, the rope of the length is set to the number of iterations
	 * 		needed to reach that intersect point, and the rope is added to the array of ropes
	 * 6. The function returns a new array of platforms, now with ropes added
	*/

	private static PlatformBase[] GenerateRopes (PlatformBase[] platforms, int[,] level) {
		PlatformBase[] addRopes = platforms;

		int maxRopeLength = 100;

		foreach (PlatformBase pb in addRopes) {
			int ropes = 1;

			pb.Ropes = new RopeBase[ropes];

			for (int r = 0; r < ropes; r++) {
				int xRopePos = Random.Range (0, pb.Width);

				//iterate through the level array vertically
				for (int i = 1; i <= maxRopeLength; i++) {
					int xPos = xRopePos + (int)pb.Pivot.x;
					int yPos = (int)pb.Pivot.y - i;

					//if we are at the bottom of the array, set the rope length to the max rope length
					if (yPos <= 0) {
						RopeBase rb = new RopeBase (xRopePos, maxRopeLength);

						pb.Ropes [r] = rb;

						break;
					}

					//if we are anywhere else, do the following test
					if (level [xPos, yPos] == 1) {
						//hit tile, rope length = i, exit

						RopeBase rb = new RopeBase (xRopePos, i);

						pb.Ropes [r] = rb;

						break;
					}

					//if we still haven't hit anything, the rope length is the maxRope length
					if (i == maxRopeLength) {
						RopeBase rb = new RopeBase (xRopePos, maxRopeLength);

						pb.Ropes [r] = rb;
					}
				}
			}
		}

		return addRopes;
	}
}

//This class is static, and so it can be accessed at any time without reference
public static class Level{
	public static PlatformBase[] platforms;
	public static PlatformObjectData StartGate;
	public static PlatformObjectData EndGate;
}