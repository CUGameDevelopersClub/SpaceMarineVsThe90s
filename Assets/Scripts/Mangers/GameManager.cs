using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

	public static int chaos = 0;
	public static int level = 1;

	//Ready for the next level?
	public static void NextLevel() {
		chaos = 0;
		level++;
		//GENERATE NEW LEVEL

		//LevelGenerator.GenerateLevel (LevelPrototype);
		//LevelMaker.CreateLevel ();
	}

	//Adds points
	public static void AddChaos(int pts) {
		chaos += pts;
	}
}