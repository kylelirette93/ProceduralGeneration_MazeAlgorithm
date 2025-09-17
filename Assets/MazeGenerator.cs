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
    GameObject[,] Map;

    // Maze properties.
    public int height;
    public int width;
    Vector3 startingPosition;
    Vector3 currentPosition;
    bool[,] isVisited;
    int steps = 100;

    private void Start()
    {
        // Starting position for map.
        InitializeBoolMap(15, 15);
        startingPosition = new Vector3(width / 2, 0, height / 2);
        GenerateMap();
    }

    private Vector3 RandomWalk(Vector3 startingPos)
    {
        Vector3 nextMove = startingPos;

            int randomDirection = Random.Range(0, 4);
            switch (randomDirection)
            {
                case 1:
                    nextMove = new Vector3(startingPos.x + 1, 0, startingPos.z);
                    break;
                case 2:
                    nextMove = new Vector3(startingPos.x - 1, 0, startingPos.z);
                    break;
                case 3:
                    nextMove = new Vector3(startingPos.x, 0, startingPos.z + 1);
                    break;
                case 4:
                    nextMove = new Vector3(startingPos.x, 0, startingPos.z - 1);
                    break;
            }
        return nextMove;
        

    }

    private void InitializeBoolMap(int height, int width)
    {
        isVisited = new bool[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                isVisited[y, x] = false;
            }
        }
    }

    private void GenerateMap()
    {
        for (int i = 0; i < steps; i++)
        {
            startingPosition = RandomWalk(startingPosition);
            if (isVisited[(int)startingPosition.x, (int)startingPosition.y] == false)
            {
                isVisited[(int)startingPosition.y, (int)startingPosition.x] = true;
                GameObject floorTile = Instantiate(FloorTile, startingPosition, Quaternion.identity);
            }
        }
    }

    
    
}
