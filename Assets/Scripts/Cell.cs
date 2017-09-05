using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int index;
    public bool visited;
    public Cell[] neighbors;
    public GameObject[] walls;

    void OnDrawGizmosSelected()
    {
        for(int i = 0;i < walls.Length;i++)
        {
            if(walls[i])
                Gizmos.DrawLine(transform.position, walls[i].transform.position);
        }
            
    }

    public bool AllVisited()
    {
        //TODO Can be simplified
        bool top = neighbors[0].visited;
        bool bottom = neighbors[1].visited;
        bool left = neighbors[2].visited;
        bool right = neighbors[3].visited;

        return top && bottom && left && right;
    }
}
