using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject floorTile;
    public GameObject wallTile;
    bool[,] isVisited;
    List<Vector3Int> path = new List<Vector3Int>();
    Vector3Int startCell;
    Vector3Int exitCell;
    Vector3Int currentCell;
    public int height;
    public int width;

    private void Start()
    {
        GenerateMaze();
    }

    private void ClearMaze()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        path.Clear();
        isVisited = null;
    }

    public void GenerateMaze()
    {
        ClearMaze();
        isVisited = new bool[(2 * width) + 1, (2 * height) + 1];
        // Pick a cell to start from.
        startCell = new Vector3Int(1, 0, 1);
        // Pick a cell to exit from.
        exitCell = new Vector3Int((2 * width), 0, (2 * height));
        // Mark it as visitied.
        isVisited[startCell.x, startCell.y] = true;
        // Add it to the path.
        path.Add(startCell);
        DepthFirstSearch();

        // Open up an exit.
        isVisited[exitCell.x, exitCell.z] = true;
        isVisited[exitCell.x, exitCell.z - 1] = true;
        isVisited[exitCell.x, exitCell.z - 2] = true;

        DrawMap();
    }

    void DepthFirstSearch()
    {
        while (path.Count > 0)
        {
            // Get last cell added to path.
            currentCell = path[path.Count - 1];
            // Get unvisited neighbours.
            List<Vector3Int> unvisitedNeighbours = GetUnvisitedNeighbours(currentCell);

            if (unvisitedNeighbours.Count > 0)
            {
                // Pick a random neighbour to move to.
                int randomChoice = Random.Range(0, unvisitedNeighbours.Count);
                Vector3Int chosenNeighbour = unvisitedNeighbours[randomChoice];
                // Remove wall to carve a path.
                RemoveWall(currentCell, chosenNeighbour);
                // Mark the current neighbour as visited and add to path.
                isVisited[chosenNeighbour.x, chosenNeighbour.y] = true;
                path.Add(chosenNeighbour);
            }
            else
            {
                // Dead end. Remove last cell visited. If there are no cells left, the loop ends.
                path.RemoveAt(path.Count - 1);
            }
        }
    }

    List<Vector3Int> GetUnvisitedNeighbours(Vector3Int currentCell)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();
        // Get a list of the next neighbours over if its not visited.

        if (currentCell.x + 2 < (2 * width) && !isVisited[currentCell.x + 2, currentCell.y])
        {
            neighbours.Add(new Vector3Int(currentCell.x + 2, currentCell.y));
        }

        // Left
        if (currentCell.x - 2 >= 1 && !isVisited[currentCell.x - 2, currentCell.y])
        {
            neighbours.Add(new Vector3Int(currentCell.x - 2, currentCell.y));
        }

        // Up
        if (currentCell.y + 2 < (2 * height) && !isVisited[currentCell.x, currentCell.y + 2])
        {
            neighbours.Add(new Vector3Int(currentCell.x, currentCell.y + 2));
        }

        // Down
        if (currentCell.y - 2 >= 1 && !isVisited[currentCell.x, currentCell.y - 2])
        {
            neighbours.Add(new Vector3Int(currentCell.x, currentCell.y - 2));
        }
        return neighbours;
    }

    private void RemoveWall(Vector3Int currentCell, Vector3Int chosenNeighbour)
    {
        // Remove the wall between current cell and next neighbour.
        Vector3Int wallToRemove = (currentCell + chosenNeighbour) / 2;
        isVisited[wallToRemove.x, wallToRemove.y] = true;
    }
    public void DrawMap()
    {
        for (int y = 0; y < (2 * height) + 1; y++)
        {
            for (int x = 0; x < (2 * width) + 1; x++)
            {
                if (isVisited[x, y])
                {
                    GameObject FloorTile = Instantiate(floorTile, new Vector3(x, -1, y), Quaternion.identity);
                    FloorTile.transform.parent = this.transform;
                }
                else
                {
                    GameObject WallTile = Instantiate(wallTile, new Vector3(x, 0, y), Quaternion.identity);
                    WallTile.transform.parent = this.transform;
                }
            }
        }
    }
}
