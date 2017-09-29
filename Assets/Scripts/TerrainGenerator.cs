using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {


    public GameObject wallPrefab;
    public GameObject ropePrefab;
    public GameObject startPrefab;
    public GameObject exitPrefab;

    int[,] map;

    List<Platform> platforms = new List<Platform>();

	// Use this for initialization
	void Start () {
        gen();
        spawn();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void gen()
    {
        //set size
        map = new int[100, 100];

        //outer wall
        for (int x = 0; x < map.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < map.GetLength(1) - 1; y++)
            {
                if (x == 0 || y == 0 || x == map.GetLength(0) - 2 || y == map.GetLength(1) - 2)
                {
                    
                    map[x, y] = 1;
                }
            }
        }

        //start everything from the middle. Note that this changes later
        Vector2 startPos = new Vector2(map.GetLength(0) / 2, map.GetLength(1) / 2);

        

        


        

        int px = (int)startPos.x;
        int py = (int)startPos.y - 1;
        //platform sizes
        int minSize = 10;
        int maxSize = 25;

        //starts from the middle
        Platform newPlat = new Platform(px, py, Random.Range(minSize, maxSize), 1, false);

        platforms.Add(newPlat);


        List<Platform> canUse = new List<Platform>();

        canUse.Add(newPlat);

        
        //gens platforms and ropes
        int i = 80;
        while (i > 0)
        {
            //get platform to use
            Platform platInUse = canUse[Random.Range(0, canUse.Count)];

            //rope Gen
            //Note algorithm could be incorrect. If so reverse
            int t = Random.Range(1, 4);
            if (platInUse.type == 1)
            {

                if (t == 1 || t == 2)
                {

                    int distY = 0;
                    if (platInUse.startingY > map.GetLength(1) - maxSize - 1)
                    {
                        distY = Random.Range(minSize, map.GetLength(1) - platInUse.startingY - 1);
                    }
                    else
                    {
                        distY = Random.Range(minSize, 15);
                    }


                    newPlat = new Platform(Random.Range(platInUse.startingX + 1, platInUse.startingX + platInUse.distance - 1),
                        platInUse.startingY - distY, distY, 2, true);
                    canUse.Add(newPlat);
                    platforms.Add(newPlat);
                }
                if(t == 2 || t == 3)
                {
                    int distY = 0;
                    if (platInUse.startingY < maxSize+1)
                    {
                        distY = Random.Range(1, platInUse.startingY);
                    }
                    else
                    {
                        distY = Random.Range(minSize, maxSize);
                    }
                    newPlat = new Platform(Random.Range(platInUse.startingX + 1, platInUse.startingX + platInUse.distance - 1),
                        platInUse.startingY, distY, 2, false);
                    canUse.Add(newPlat);
                    platforms.Add(newPlat);
                }
                canUse.Remove(platInUse);
            }

            //wall gen
            //note algorithm could be incorrect. If so reverse
            if (platInUse.type == 2)
            {

                int distX = 0;
                if (t == 1 || t == 2)
                {
                    if (platInUse.startingX > map.GetLength(0) - 16)
                    {
                        distX = Random.Range(5, map.GetLength(0) - platInUse.startingX - 1);
                    }
                    else
                    {
                        distX = Random.Range(5, 15);
                    }

                    if (platInUse.useTop)
                    {
                        newPlat = new Platform(platInUse.startingX - distX,
                            Random.Range(platInUse.startingY + 1, platInUse.startingY + (platInUse.distance / 2) - 1),
                            distX, 1, false);
                    }
                    else
                    {
                        newPlat = new Platform(platInUse.startingX - distX,
                            Random.Range(platInUse.startingY + (platInUse.distance / 2) + 1, platInUse.startingY + platInUse.distance - 1),
                            distX, 1, false);
                    }
                    
                    canUse.Add(newPlat);
                    platforms.Add(newPlat);
                }
                if(t == 2 || t == 3)
                {
                    if (platInUse.startingX < 16)
                    {
                        distX = Random.Range(1, platInUse.startingX);
                    }
                    else
                    {
                        distX = Random.Range(5, 15);
                    }

                    if (platInUse.useTop)
                    {
                        newPlat = new Platform(platInUse.startingX,
                            Random.Range(platInUse.startingY + 1, platInUse.startingY + (platInUse.distance / 2) - 1),
                            distX, 1, false);
                    }
                    else
                    {
                        newPlat = new Platform(platInUse.startingX,
                            Random.Range(platInUse.startingY + 1 + (platInUse.distance / 2), platInUse.startingY + platInUse.distance - 1),
                            distX, 1, false);
                    }
                    
                    canUse.Add(newPlat);
                    platforms.Add(newPlat);
                }
                canUse.Remove(platInUse);

            }
            

            


            i--;
        }


        //put platforms into the 2d array
        foreach (Platform plat in platforms)
        {
            int xp = plat.startingX;
            int yp = plat.startingY;

            int dist = 0;
            //ropes
            if (plat.type == 2)
            {
                while (dist < plat.distance)
                {
                    if(yp + dist >= 0 && yp + dist < map.GetLength(1) - 1 && xp >= 0 && xp < map.GetLength(0) - 1)
                    {
                        
                        if (map[xp, yp + dist] == 1 || map[xp, yp + dist] == 5)
                        {
                            map[xp, yp + dist] = 5;
                        }
                        else
                        {
                            map[xp, yp + dist] = 2;
                        }
                    }
                        
                    

                    dist++;
                }
            }
            //walls
            if (plat.type == 1)
            {
                while (dist < plat.distance)
                {
                    if (xp + dist >= 0 && xp + dist < map.GetLength(0) - 1 && yp >= 0 && yp < map.GetLength(1) - 1)
                    {
                        if (map[xp + dist, yp] == 2 || map[xp + dist, yp] == 5)
                        {
                            map[xp + dist, yp] = 5;
                        }
                        else
                        {
                            map[xp + dist, yp] = 1;
                        }
                        
                    }
                    

                    dist++;
                }
            }


        }

        //get the start spot
        startPos = getEmptySpot();
        Vector2 endPos = Vector2.zero;
        //get the exit spot
        bool isDone = false;
        while (!isDone)
        {
            endPos = getEmptySpot();
            
            if (Vector2.Distance(startPos, endPos) > map.GetLength(0) / 6)
            {
                isDone = true;
            }
        }

        //add spots to map
        map[(int)startPos.x, (int)startPos.y] = 3;
        map[(int)endPos.x, (int)endPos.y] = 4;


    }

    //spawn all the blocks
    //may want to change to a single object
    void spawn()
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                //wall
                if (map[x, y] == 1)
                {
                    Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                //rope
                if (map[x, y] == 2)
                {
                    Instantiate(ropePrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                //start
                if (map[x, y] == 3)
                {
                    Instantiate(startPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                //exit
                if (map[x, y] == 4)
                {
                    Instantiate(exitPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                //wall and rope
                if (map[x, y] == 5)
                {
                    Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    Instantiate(ropePrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    Vector2 getEmptySpot()
    {

        
        // returns when there is a wall with an empty space above.
        bool isDone = false;
        while (!isDone)
        {
            for (int x = 1; x < map.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < map.GetLength(1) - 1; y++)
                {
                    if (map[x, y] == 1 && map[x, y + 1] == 0)
                    {
                        if (Random.value < 0.001f)
                        {
                            isDone = true;
                            return new Vector2(x, y + 1);
                        }
                    }



                }
            }
        }

        return Vector2.zero;

    }

    

}

//platforms can be walls or roped
public class Platform
{

    public int startingX, startingY;
    public int distance;
    //1 = wall
    //2 = rope
    public int type;

    public bool useTop;

    public Platform(int startingX, int startingY, int distance, int type, bool useTop)
    {
        this.startingX = startingX;
        this.startingY = startingY;
        this.distance = distance;
        this.type = type;
        this.useTop = useTop;

        
    }
}
