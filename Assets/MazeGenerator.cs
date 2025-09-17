using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // Maze generation.
    public GameObject FloorTile;
    public GameObject WallTile;
    public GameObject[,] Map;

    // Maze properties.
    public int height;
    public int width;
    int steps = 300;
    Vector3 startingPosition;
    Vector3 currentPosition;

    private void Start()
    {
        startingPosition = new Vector3(height / 2, width / 2);
        GenerateMap(height, width, currentPosition);
    }

    private void Update()
    {
        currentPosition = RandomWalk(startingPosition);
        GenerateMap(height, width, currentPosition);
    }

    private Vector3 RandomWalk(Vector3 startingPos)
    {
        Vector3 nextMove = startingPos;

            int randomDirection = Random.Range(0, 4);
            switch (randomDirection)
            {
                case 1:
                    nextMove = new Vector3(startingPos.x + 1, 0, startingPos.y);
                    break;
                case 2:
                    nextMove = new Vector3(startingPos.x - 1, 0, startingPos.y);
                    break;
                case 3:
                    nextMove = new Vector3(startingPos.x, 0, startingPos.y + 1);
                    break;
                case 4:
                    nextMove = new Vector3(startingPos.x, 0, startingPos.y - 1);
                    break;
            }
        return nextMove;
        

    }

    private void GenerateMap(int height, int width, Vector3 walkerPos)
    {
        GameObject[,] newMap = new GameObject[height, width];
        for (int y = 0; y < newMap.GetLength(0); y++)
        {
            for (int x = 0; x < newMap.GetLength(1); x++)
            {
                if (walkerPos.x == x && walkerPos.y == y)
                {
                    GameObject floorTile = Instantiate(FloorTile, walkerPos, Quaternion.identity);
                    newMap[y, x] = floorTile;
                }
                else
                {
                    GameObject wallTile = Instantiate(WallTile, new Vector3(x, 0, y), Quaternion.identity);
                    newMap[y, x] = wallTile;
                }
                
            }
        }
    }

    
    
}
