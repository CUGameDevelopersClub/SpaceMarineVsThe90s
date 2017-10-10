using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour {
	public LevelPrototype levelPrototype;

	public GameObject tile;
	public GameObject rope;

    private void Awake (){
        DontDestroyOnLoad(transform.gameObject); 
    }

	public void CreateLevel () {
		DestroyLevel ();

		LevelGenerator.GenerateLevel (levelPrototype);


		GameObject startGate = Instantiate (Level.StartGate.Object) as GameObject;
        startGate.transform.position = Level.StartGate.position;
        startGate.transform.parent = transform;

		GameObject endGate = Instantiate (Level.EndGate.Object) as GameObject;
        endGate.transform.position = Level.EndGate.position;
        endGate.transform.parent = transform;

		for (int i = 0; i < Level.platforms.Length; i++) {
			GameObject platform = Instantiate (tile, new Vector2 (Level.platforms[i].Pivot.x + Level.platforms[i].Width * 0.5f, Level.platforms[i].Pivot.y + 0.5f), Quaternion.identity, transform);

			SpriteRenderer sr = platform.GetComponent <SpriteRenderer> ();
			sr.size = new Vector2 (Level.platforms[i].Width, 1);

			BoxCollider2D bcp = platform.GetComponent <BoxCollider2D> ();
			bcp.size = sr.size;

			for (int r = 0; r < Level.platforms[i].Ropes.Length; r++) {
				GameObject R = Instantiate (rope, new Vector2 (Level.platforms[i].Pivot.x + Level.platforms[i].Ropes[r].PlatformPosition + 0.5f, Level.platforms[i].Pivot.y - Level.platforms[i].Ropes[r].Length * 0.5f + 1), Quaternion.identity, platform.transform);

				SpriteRenderer rope_sr = R.GetComponent <SpriteRenderer> ();
				rope_sr.size = new Vector2 (0.25f, Level.platforms[i].Ropes [r].Length);

				BoxCollider2D bcr = R.GetComponent <BoxCollider2D> ();
				bcr.size = rope_sr.size + new Vector2 (0, 1);
			}
		}
	}

	public void DestroyLevel () {
		List<GameObject> children = new List<GameObject> ();

		foreach (Transform child in transform)
			children.Add (child.gameObject);
		
        if (!Application.isPlaying)
		    children.ForEach (x => DestroyImmediate (x));
        else
            children.ForEach(x => Destroy(x));
    }
}
