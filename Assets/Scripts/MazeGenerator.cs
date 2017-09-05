using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
    public int width, height;
    public float scale;
    public Cell[] cells;

    public Material floorMaterial, wallMaterial;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    [ContextMenu("Delete Children")]
    public void DeleteChildren()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    [ContextMenu("Generate Test Maze")]
    public void BuildMaze()
    {
        GenerateMaze(width, height);
    }

    void GenerateMaze(int width, int height)
    {
        DeleteChildren();

        transform.localScale = new Vector3(scale, scale, scale);

        for (int y = 0; y < height;y++)
        {
            for(int x = 0; x < width;x++)
            {
                Vector3 position = new Vector3((x - (width / 2)) * scale, 0, (y - (height / 2)) * scale);

                // Create the quad for floor
                GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Quad);
                floor.GetComponent<MeshRenderer>().material = floorMaterial;
                floor.transform.position = position;
                floor.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                floor.transform.parent = this.transform;
                floor.name = "cell " + (x + (y * width) + " [" + x + ":" + y + "]");

                // Create the walls
                CreateWalls(floor.AddComponent<Cell>());
            }
        }

        // Build cells structure
    }

    void RecursiveBacktracker(Cell cell)
    {
        if (cell.visited)
            return;

        Cell[] neighbors = cell.neighbors;

        while(!cell.AllVisited())
        {
            int i = UnityEngine.Random.Range(0, neighbors.Length);

            if(neighbors[i])
            {
                RecursiveBacktracker(neighbors[i]);

                RemoveWall(i, cell);    // BROKE MY BRAIN

                neighbors[i] = null;
            }
        }

        cell.visited = true;
    }

    private void RemoveWall(int i, Cell cell)
    {

    }

    void CreateWalls(Cell cell)
    {
        cell.neighbors = new Cell[4];
        cell.walls = new GameObject[4];

        // SORRY FOR THIS CREATEWALLS MESS
        // Biggest mess is the mixture between Local and Global Rotation. But it just werks (tm)

        GameObject top = GameObject.CreatePrimitive(PrimitiveType.Quad);
        top.GetComponent<MeshRenderer>().material = wallMaterial;
        top.transform.parent = cell.transform;
        top.transform.localPosition = new Vector3(0, scale/2, -scale/2);
        top.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        top.name = "wall top";
        cell.walls[0] = top;

        GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Quad);
        bottom.GetComponent<MeshRenderer>().material = wallMaterial;
        bottom.transform.parent = cell.transform;
        bottom.transform.localPosition = new Vector3(0, -scale/2, -scale/2);
        bottom.transform.rotation = Quaternion.Euler(new Vector3(-180, 0, 0));
        bottom.name = "wall bottom";
        cell.walls[1] = bottom;

        GameObject left = GameObject.CreatePrimitive(PrimitiveType.Quad);
        left.GetComponent<MeshRenderer>().material = wallMaterial;
        left.transform.parent = cell.transform;
        left.transform.localPosition = new Vector3(-scale / 2, 0, -scale / 2);
        left.transform.localRotation = Quaternion.Euler(new Vector3(0,-90, 0));
        left.name = "wall left";
        cell.walls[2] = left;

        GameObject right = GameObject.CreatePrimitive(PrimitiveType.Quad);
        right.GetComponent<MeshRenderer>().material = wallMaterial;
        right.transform.parent = cell.transform;
        right.transform.localPosition = new Vector3(scale / 2, 0, -scale / 2);
        right.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        right.name = "wall right";
        cell.walls[3] = right;
    }
}
