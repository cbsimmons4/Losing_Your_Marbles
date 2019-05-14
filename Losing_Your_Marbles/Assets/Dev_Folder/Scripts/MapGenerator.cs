using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

/*
 * Procedural cave generation code
 * and algorithm from Sebastian Lague
 * "[Unity] Procedural Cave Generation" Episode 1-9
 * https://youtu.be/v7yyZZjF1z4?list=PLFt_AvWsXl0eZgMK_DT5_biRkWXftAOf9
 */

public class MapGenerator : MonoBehaviour {

    public GameObject groundRock;

    public Transform Marble;

    public Transform Enemy;

    public Transform Item;

    [Range(0, 250)]
    public static int enemy_cap;

    [Range(1, 50)]
    public static  int num_marbles;

    [Range(100, 255)]
    public static  int width;

    [Range(100, 255)]
    public static int height;

    [Range(5, 10)]
    public static int passageWidth;

	public static string seed;

	public static bool useRandomSeed;

	[Range(35,55)]
	public static int  randomFillPercent;

	private int[,] map;

    [Range(20, 100)]
    public  static int wallThresholdSize;
    
    [Range(20, 100)]
    public static int roomThresholdSize;

    [Range(0, 3)]
    public static int forest_density;

    public static bool disableMinimap;

    private int center_x;
    private int center_z;

    public System.Random pseudoRandom;

    private List<KeyValuePair<int, int>> open_spaces;

    int marbles_spawned;

    public List<GameObject> obsticles;

    public List<GameObject> decorations;
    
    void Awake() {


       // Debug.Log(width);

        if (useRandomSeed || seed == null)
        {
            pseudoRandom = new System.Random();
        }
        else
        {
            pseudoRandom = new System.Random(seed.GetHashCode());
        }

        if (disableMinimap)
        {
            GameObject.Find("Minimap").SetActive(false);
        }

        this.center_x = width/2;
        this.center_z = height/2;
		GenerateMap();

	}



    private void add_decorations() {
        foreach (KeyValuePair<int, int> space in this.open_spaces)
        {
            if (Random.Range(1, 70) < forest_density + 1)
            {
                Vector3 cur_pos = new Vector3((space.Key - this.center_x) * 2, 0, (space.Value - this.center_z) * 2);
                GameObject obj = this.decorations[Random.Range(0, this.decorations.Count)];
                GameObject cur = Instantiate(obj, cur_pos, obj.transform.rotation);
                cur.transform.Rotate(this.pseudoRandom.Next(0, 2), this.pseudoRandom.Next(0, 360), this.pseudoRandom.Next(0, 2));
                cur.transform.parent = GameObject.Find("NavCreators").transform;

            }
        }
    }

    private void add_obsticles() {

        List<KeyValuePair<int, int>> to_remove = new List<KeyValuePair<int, int>> ();

        foreach (KeyValuePair<int, int> space in  this.open_spaces)
        {
            if (Random.Range(1,70) < forest_density + 1)
            {
                Vector3 cur_pos = new Vector3((space.Key - this.center_x) * 2, 0, (space.Value - this.center_z) * 2) ;
                GameObject obj = this.obsticles[Random.Range(0, this.obsticles.Count)];
                GameObject cur = Instantiate(obj, cur_pos - new Vector3(0, 0.25f,0), obj.transform.rotation);
                cur.transform.Rotate(this.pseudoRandom.Next(0, 2), this.pseudoRandom.Next(0, 360), this.pseudoRandom.Next(0, 2));
                cur.transform.parent = GameObject.Find("NavCreators").transform;
                to_remove.Add(space);
            }
        }
        foreach (KeyValuePair<int, int> space in to_remove)
        {
            this.open_spaces.Remove(space);
        }
    }


    private void Spawn_Marbles() {

        List<KeyValuePair<int, int>> spaces = new List<KeyValuePair<int, int>>(this.open_spaces);
        this.marbles_spawned = 0;

        for ( int i = 0; i < num_marbles; i++) {
            if (spaces.Count > 1) {
                KeyValuePair<int, int> cur_position = spaces[this.pseudoRandom.Next(0, open_spaces.Count)];
                spaces.Remove(cur_position);
                float x = cur_position.Key;
                float y = cur_position.Value;
                Transform cur = Instantiate(this.Marble, new Vector3((x - this.center_x) * 2, .5f, (y - this.center_z) * 2), this.Marble.transform.rotation);
                cur.transform.parent = GameObject.Find("Marbles").transform;
                this.marbles_spawned++;
            }
        }
    }

    public List<KeyValuePair<int, int>> Get_OS() 
    {
        return open_spaces;
    }

    void GenerateMap() {
		map = new int[width,height];
		RandomFillMap();

		for (int i = 0; i < 10; i ++) {
			SmoothMap();
		}

        ProcessMap ();
        makeSpawnArea();

        int borderSize = 1;
		int[,] borderedMap = new int[width + borderSize * 2,height + borderSize * 2];

		for (int x = 0; x < borderedMap.GetLength(0); x ++) {
			for (int y = 0; y < borderedMap.GetLength(1); y ++) {
				if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize) {
					borderedMap[x,y] = map[x-borderSize,y-borderSize];
				}
                else {
					borderedMap[x,y] = 1;
				}
			}
		}

        map = borderedMap;

        this.open_spaces = this.Get_OpenSpaces();
       
        this.Place_Walls(borderedMap);
        this.add_obsticles();

        var ground = GameObject.Find("Ground").GetComponent<NavMeshSurface>();
        ground.BuildNavMesh();
        add_decorations();
        this.Spawn_Marbles();

    }

    private List<KeyValuePair<int, int>> Get_OpenSpaces()
    {
        List<KeyValuePair<int, int>> os = new List<KeyValuePair<int, int>>();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            { 
            if (map[x, y] == 0 && !(Vector3.Distance(new Vector3(x, 0, y), new Vector3(center_x, 0, center_z)) < 15f)
                && !(map[x+1, y] == 1 || map[x - 1, y] == 1 || map[x, y + 1 ] == 1 || map[x, y - 1] == 1) ) {
                    os.Add(new KeyValuePair<int, int>(x, y));
                }
            }

        }
        return os;
    }

    void makeSpawnArea()
    { 
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (Vector3.Distance(new Vector3(x, 0, y), new Vector3(center_x, 0, center_z)) < 20f)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    void Place_Walls(int[,] borderedMap) 
    {
        for (int x = 0; x < borderedMap.GetLength(0); x++)
        {
            for (int y = 0; y < borderedMap.GetLength(1); y++)
            {

                if (borderedMap[x, y] == 1)
                {
                    if (x == 0 || y == 0 || x == borderedMap.GetLength(0) - 1 || y == borderedMap.GetLength(1) - 1
                        || borderedMap[x + 1, y] == 0 || borderedMap[x - 1, y] == 0 || borderedMap[x, y + 1] == 0 || borderedMap[x, y - 1] == 0) {
                        Vector3 cur_pos = new Vector3((x - this.center_x) * 2, .5f, (y - this.center_z) * 2);
                        GameObject cur = Instantiate(groundRock,cur_pos, groundRock.transform.rotation);
                        cur.transform.Rotate(this.pseudoRandom.Next(0, 2), this.pseudoRandom.Next(0, 360), this.pseudoRandom.Next(0, 2));
                        cur.transform.parent = GameObject.Find("NavCreators").transform;
                    }

                    var mini = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    mini.transform.position = new Vector3( ((x - this.center_x) * 2 ), 2009, (y - this.center_z) * 2);
                    mini.transform.parent = GameObject.Find("MiniMapWalls").transform;
                    mini.transform.localScale = 2 * mini.transform.localScale;
                    mini.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }

    }

    void ProcessMap() {
		List<List<Coord>> wallRegions = GetRegions (1);
		

		foreach (List<Coord> wallRegion in wallRegions) {
			if (wallRegion.Count < wallThresholdSize) {
				foreach (Coord tile in wallRegion) {
					map[tile.tileX,tile.tileY] = 0;
				}
			}
		}

		List<List<Coord>> roomRegions = GetRegions (0);
		List<Room> survivingRooms = new List<Room> ();
		
		foreach (List<Coord> roomRegion in roomRegions) {
			if (roomRegion.Count < roomThresholdSize) {
				foreach (Coord tile in roomRegion) {
					map[tile.tileX,tile.tileY] = 1;
				}
			}
			else {
				survivingRooms.Add(new Room(roomRegion, map));
			}
		}
		survivingRooms.Sort ();
		survivingRooms[0].isMainRoom = true;
		survivingRooms[0].isAccessibleFromMainRoom = true;

		ConnectClosestRooms (survivingRooms);
	}

	void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false) {

		List<Room> roomListA = new List<Room> ();
		List<Room> roomListB = new List<Room> ();

		if (forceAccessibilityFromMainRoom) {
			foreach (Room room in allRooms) {
				if (room.isAccessibleFromMainRoom) {
					roomListB.Add (room);
				} else {
					roomListA.Add (room);
				}
			}
		} else {
			roomListA = allRooms;
			roomListB = allRooms;
		}

		int bestDistance = 0;
		Coord bestTileA = new Coord ();
		Coord bestTileB = new Coord ();
		Room bestRoomA = new Room ();
		Room bestRoomB = new Room ();
		bool possibleConnectionFound = false;

		foreach (Room roomA in roomListA) {
			if (!forceAccessibilityFromMainRoom) {
				possibleConnectionFound = false;
				if (roomA.connectedRooms.Count > 0) {
					continue;
				}
			}

			foreach (Room roomB in roomListB) {
				if (roomA == roomB || roomA.IsConnected(roomB)) {
					continue;
				}
			
				for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA ++) {
					for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB ++) {
						Coord tileA = roomA.edgeTiles[tileIndexA];
						Coord tileB = roomB.edgeTiles[tileIndexB];
						int distanceBetweenRooms = (int)(Mathf.Pow (tileA.tileX-tileB.tileX,2) + Mathf.Pow (tileA.tileY-tileB.tileY,2));

						if (distanceBetweenRooms < bestDistance || !possibleConnectionFound) {
							bestDistance = distanceBetweenRooms;
							possibleConnectionFound = true;
							bestTileA = tileA;
							bestTileB = tileB;
							bestRoomA = roomA;
							bestRoomB = roomB;
						}
					}
				}
			}
			if (possibleConnectionFound && !forceAccessibilityFromMainRoom) {
				CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			}
		}

		if (possibleConnectionFound && forceAccessibilityFromMainRoom) {
			CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			ConnectClosestRooms(allRooms, true);
		}

		if (!forceAccessibilityFromMainRoom) {
			ConnectClosestRooms(allRooms, true);
		}
	}

	void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB) {
		Room.ConnectRooms (roomA, roomB);
		//Debug.DrawLine (CoordToWorldPoint (tileA), CoordToWorldPoint (tileB), Color.green, 100);

		List<Coord> line = GetLine (tileA, tileB);
		foreach (Coord c in line) {
			DrawCircle(c,passageWidth);
		}
	}

	void DrawCircle(Coord c, int r) {
		for (int x = -r; x <= r; x++) {
			for (int y = -r; y <= r; y++) {
				if (x*x + y*y <= r*r) {
					int drawX = c.tileX + x;
					int drawY = c.tileY + y;
					if (IsInMapRange(drawX, drawY)) {
						map[drawX,drawY] = 0;
					}
				}
			}
		}
	}

	List<Coord> GetLine(Coord from, Coord to) {
		List<Coord> line = new List<Coord> ();

		int x = from.tileX;
		int y = from.tileY;

		int dx = to.tileX - from.tileX;
		int dy = to.tileY - from.tileY;

		bool inverted = false;
		int step = Math.Sign (dx);
		int gradientStep = Math.Sign (dy);

		int longest = Mathf.Abs (dx);
		int shortest = Mathf.Abs (dy);

		if (longest < shortest) {
			inverted = true;
			longest = Mathf.Abs(dy);
			shortest = Mathf.Abs(dx);

			step = Math.Sign (dy);
			gradientStep = Math.Sign (dx);
		}

		int gradientAccumulation = longest / 2;
		for (int i =0; i < longest; i ++) {
			line.Add(new Coord(x,y));

			if (inverted) {
				y += step;
			}
			else {
				x += step;
			}

			gradientAccumulation += shortest;
			if (gradientAccumulation >= longest) {
				if (inverted) {
					x += gradientStep;
				}
				else {
					y += gradientStep;
				}
				gradientAccumulation -= longest;
			}
		}

		return line;
	}

	Vector3 CoordToWorldPoint(Coord tile) {
		return new Vector3 (-width / 2 + .5f + tile.tileX, 2, -height / 2 + .5f + tile.tileY);
	}

	List<List<Coord>> GetRegions(int tileType) {
		List<List<Coord>> regions = new List<List<Coord>> ();
		int[,] mapFlags = new int[width,height];

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
					List<Coord> newRegion = GetRegionTiles(x,y);
					regions.Add(newRegion);

					foreach (Coord tile in newRegion) {
						mapFlags[tile.tileX, tile.tileY] = 1;
					}
				}
			}
		}

		return regions;
	}

	List<Coord> GetRegionTiles(int startX, int startY) {
		List<Coord> tiles = new List<Coord> ();
		int[,] mapFlags = new int[width,height];
		int tileType = map [startX, startY];

		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (new Coord (startX, startY));
		mapFlags [startX, startY] = 1;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();
			tiles.Add(tile);

			for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
				for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
					if (IsInMapRange(x,y) && (y == tile.tileY || x == tile.tileX)) {
						if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
							mapFlags[x,y] = 1;
							queue.Enqueue(new Coord(x,y));
						}
					}
				}
			}
		}
		return tiles;
	}

	bool IsInMapRange(int x, int y) {
		return x >= 0 && x < width && y >= 0 && y < height;
	}


	void RandomFillMap() {
        for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
			}
		}
	}

	void SmoothMap() {

        for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);


                if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;

			}
		}
    }

	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (IsInMapRange(neighbourX,neighbourY)) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}

	struct Coord {
		public int tileX;
		public int tileY;

		public Coord(int x, int y) {
			tileX = x;
			tileY = y;
		}
	}

	class Room : IComparable<Room> {
		public List<Coord> tiles;
		public List<Coord> edgeTiles;
		public List<Room> connectedRooms;
		public int roomSize;
		public bool isAccessibleFromMainRoom;
		public bool isMainRoom;

		public Room() {
		}

		public Room(List<Coord> roomTiles, int[,] map) {
			tiles = roomTiles;
			roomSize = tiles.Count;
			connectedRooms = new List<Room>();

			edgeTiles = new List<Coord>();
			foreach (Coord tile in tiles) {
				for (int x = tile.tileX-1; x <= tile.tileX+1; x++) {
					for (int y = tile.tileY-1; y <= tile.tileY+1; y++) {
						if (x == tile.tileX || y == tile.tileY) {
							if (map[x,y] == 1) {
								edgeTiles.Add(tile);
							}
						}
					}
				}
			}
		}

		public void SetAccessibleFromMainRoom() {
			if (!isAccessibleFromMainRoom) {
				isAccessibleFromMainRoom = true;
				foreach (Room connectedRoom in connectedRooms) {
					connectedRoom.SetAccessibleFromMainRoom();
				}
			}
		}

		public static void ConnectRooms(Room roomA, Room roomB) {
			if (roomA.isAccessibleFromMainRoom) {
				roomB.SetAccessibleFromMainRoom ();
			} else if (roomB.isAccessibleFromMainRoom) {
				roomA.SetAccessibleFromMainRoom();
			}
			roomA.connectedRooms.Add (roomB);
			roomB.connectedRooms.Add (roomA);
		}

		public bool IsConnected(Room otherRoom) {
			return connectedRooms.Contains(otherRoom);
		}

		public int CompareTo(Room otherRoom) {
			return otherRoom.roomSize.CompareTo (roomSize);
		}
	}

    public int[,] Get_map()
    {
        return this.map;
    }

    public int GetCenterX()
    {
        return this.center_x;
    }

    public int GetCenterZ() 
    {
        return this.center_z;
    }

    public int GetHeight() 
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }

    public int Get_num_Marbles()
    {
        return num_marbles;
    }


    public void DisableMiniMap()
    {
        disableMinimap = true;
    }

    public void EnableMiniMap()
    {
        disableMinimap = false;
    }

    public int Get_Marbles_Spawned() {
        return this.marbles_spawned;
    }


    public void SetEnemyCap(int cap)
    {
        enemy_cap = cap;
    }

    public void SetNumMarbles (int num) {
        num_marbles = num;
    }

    public void SetWidth(int w)
    {
        width = w;

    }

    public void SetHeigth(int h) {
        height = h;
    }


    public void SetPassageWidth (int pw)
    {
        passageWidth = pw;
    }

    public void SetSeed (String s)
    {
        seed = s;
    }

    public void SetUseRandomSeed(bool b)
    {
        useRandomSeed = b;
    }

    public void SetRandomFillPercent (int num)
    {
        randomFillPercent = num;
    }

 
    public void SetWallThreshholdSize( int num) {
        wallThresholdSize = num;
    }

    public void SetRoomThresholdSize (int num)
    {
        roomThresholdSize = num;
    }

    public void SetForestDensity(int num)
    {
        forest_density = num;
    }

    public void SetMapStatus (bool enabled) {
        disableMinimap = !enabled;

     }
}
