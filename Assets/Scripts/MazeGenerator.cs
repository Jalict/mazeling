﻿// remember you can NOT have even numbers of height or width in this style of block maze
// to ensure we can get walls around all tunnels...  so use 21 x 13 , or 7 x 7 for examples.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class MazeGenerator: MonoBehaviour
{
    public int width, height;
    public Material[] brick;
    public AudioClip[] breaking;
    private int[,] Maze;
    private List<Vector3> pathMazes = new List<Vector3>();
    private Stack<Vector2> _tiletoTry = new Stack<Vector2>();
    private List<Vector2> offsets = new List<Vector2> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    private System.Random rnd = new System.Random();
    private int _width, _height;
    private Vector2 _currentTile;
    public String MazeString;

    public Vector2 CurrentTile
    {
        get { return _currentTile; }
        private set
        {
            if (value.x < 1 || value.x >= this.width - 1 || value.y < 1 || value.y >= this.height - 1)
            {
                throw new ArgumentException("Width and Height must be greater than 2 to make a maze");
            }
            _currentTile = value;
        }
    }
    private static MazeGenerator instance;
    public static MazeGenerator Instance
    {
        get { return instance; }
    }
    void Awake() { instance = this; }
    void Start() { MakeBlocks(); }

    // end of main program

    // ============= subroutines ============

    public void MakeBlocks()
    {

	// completely fill map with blocks 
        Maze = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Maze[x, y] = 1;
            }
        }
        CurrentTile = Vector2.one;
        _tiletoTry.Push(CurrentTile);
        Maze = CreateMaze();  // generate the maze in Maze Array.
        GameObject ptype = null;

	    int MazeTileHeight = Maze.GetUpperBound(0);
	    int MazeTileWidth = Maze.GetUpperBound(1);

	// remove blocks to make maze
        for (int y = 1; y < MazeTileHeight; y++)
        {
            for (int x = 1; x < MazeTileWidth; x++)
            {
                if (ShouldRemoveWall(y, x, MazeTileHeight, MazeTileWidth))
                {
                    Maze[y, x] = 0;
	        }
	    }
        }

        // remove blocks around center
        for (int y = MazeTileHeight / 2 - 2; y <= MazeTileHeight / 2 + 2; y++)
        {
            for (int x = MazeTileWidth / 2 - 2; x <= MazeTileWidth/ 2 + 2; x++)
            {
                Maze[y, x] = 0;
            }
        }

	// remove blocks around center
	for (int y = MazeTileHeight / 2 - 2; y <= MazeTileHeight / 2 + 2; y++)
	{
		for (int x = MazeTileWidth/ 2 - 2; x <= MazeTileWidth/ 2 + 2; x++)
		{
			Maze[y, x] = 0;
		}
	}

	// remove blocks with no neighbors to make rooms
        for (int y = 2; y <= MazeTileHeight - 2; y += 2)
        {
            for (int x = 2; x <= MazeTileWidth - 2; x += 2)
            {
		bool NeighborsEmpty =
			Maze[y - 1, x] == 0 && // top
			Maze[y, x - 1] == 0 && // left
			Maze[y + 1, x] == 0 && // bottom
			Maze[y, x + 1] == 0;   // right

                if (NeighborsEmpty)
                {
		    Maze[y, x] = 0;
		}
	    }
        }

        print(MazeString);  // added to create String

        for (int y = 0; y <= MazeTileHeight; y++)
        {
            for (int x = 0; x <= MazeTileWidth; x++)
            {
                if (Maze[y, x] == 1)
		            {
                    MazeString = MazeString + "X";  // added to create String
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.name = "[" + y + ":" + x + "]";
                    ptype.transform.position = new Vector3(x * ptype.transform.localScale.x, 0, y * ptype.transform.localScale.z);
                    if (x > 0 && x < MazeTileWidth && y > 0 && y < MazeTileHeight)
                    {
                        Wall w = ptype.AddComponent<Wall>();
                        w.wallMaterials = brick;
                        w.breakingClip = breaking;
                        ptype.tag = "Wall";
                    }
                        

                    if (brick != null) { ptype.GetComponent<Renderer>().material = brick[0]; }
                    ptype.transform.parent = transform;

		        }
		        else
                {
                    MazeString = MazeString + "0"; // added to create String
                    pathMazes.Add(new Vector3(x, 0, y));
                }
	    }
            MazeString = MazeString + "\n";  // added to create String
	}
    }


    // =======================================
    public int[,] CreateMaze()
    {

        //local variable to store neighbors to the current square as we work our way through the maze
        List<Vector2> neighbors;
        //as long as there are still tiles to try
        while (_tiletoTry.Count > 0)
        {
            //excavate the square we are on
            Maze[(int)CurrentTile.x, (int)CurrentTile.y] = 0;
            //get all valid neighbors for the new tile
            neighbors = GetValidNeighbors(CurrentTile);
            //if there are any interesting looking neighbors
            if (neighbors.Count > 0)
            {
                //remember this tile, by putting it on the stack
                _tiletoTry.Push(CurrentTile);
                //move on to a random of the neighboring tiles
                CurrentTile = neighbors[rnd.Next(neighbors.Count)];
            }
            else
            {
                //if there were no neighbors to try, we are at a dead-end toss this tile out
                //(thereby returning to a previous tile in the list to check).
                CurrentTile = _tiletoTry.Pop();
            }
        }
        print("Maze Generated ...");
        return Maze;
    }

    // ================================================
    // Get all the prospective neighboring tiles "centerTile" The tile to test
    // All and any valid neighbors</returns>
    private List<Vector2> GetValidNeighbors(Vector2 centerTile)
    {
        List<Vector2> validNeighbors = new List<Vector2>();
        //Check all four directions around the tile
        foreach (var offset in offsets)
        {
            //find the neighbor's position
            Vector2 toCheck = new Vector2(centerTile.x + offset.x, centerTile.y + offset.y);
            //make sure the tile is not on both an even X-axis and an even Y-axis
            //to ensure we can get walls around all tunnels
            if (toCheck.x % 2 == 1 || toCheck.y % 2 == 1)
            {

                //if the potential neighbor is unexcavated (==1)
                //and still has three walls intact (new territory)
                if (Maze[(int)toCheck.x, (int)toCheck.y] == 1 && HasThreeWallsIntact(toCheck))
                {

                    //add the neighbor
                    validNeighbors.Add(toCheck);
                }
            }
        }
        return validNeighbors;
    }
    // ================================================
    // Counts the number of intact walls around a tile
    //"Vector2ToCheck">The coordinates of the tile to check
    //Whether there are three intact walls (the tile has not been dug into earlier.
    private bool HasThreeWallsIntact(Vector2 Vector2ToCheck)
    {

        int intactWallCounter = 0;
        //Check all four directions around the tile
        foreach (var offset in offsets)
        {

            //find the neighbor's position
            Vector2 neighborToCheck = new Vector2(Vector2ToCheck.x + offset.x, Vector2ToCheck.y + offset.y);
            //make sure it is inside the maze, and it hasn't been dug out yet
            if (IsInside(neighborToCheck) && Maze[(int)neighborToCheck.x, (int)neighborToCheck.y] == 1)
            {
                intactWallCounter++;
            }
        }
        //tell whether three walls are intact
        return intactWallCounter == 3;
    }

    // ================================================
    private bool IsInside(Vector2 p)
    {
        //return p.x >= 0  p.y >= 0  p.x < width  p.y < height;
        return p.x >= 0 && p.y >= 0 && p.x < width && p.y < height;
    }

    private bool ShouldRemoveWall(int y, int x, int height, int width)
    {
        return x % 2 != y % 2 && rnd.Next(100) < 20;
    }

    public Vector3 GetRandomCorner()
    {
        int num = UnityEngine.Random.Range(0, 3);

        // Too lazy to calc don't judge
        switch (num)
        {
            case 0:
                return new Vector3(1, 2, 1);            
            case 1:
                return new Vector3(1, 2, 29);
            case 2:
                return new Vector3(29, 2, 29);
            case 3:
                return new Vector3(29, 2, 1);
        }

        throw new IndexOutOfRangeException("Bad random number");
    }
}
