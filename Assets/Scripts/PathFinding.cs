using UnityEngine;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour
{
    public Obstacle obstacleData;
    
    public int width = 10;
    public int height = 10;
    PathNode[,] grid;
    public Grid gridManager;

    void Awake()
    {
        // Initialize the grid based on the width and height specified in the inspector
        grid = new PathNode[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate the index in the obstacleGrid array based on the current x and y coordinates
                int index = y * width + x;

                bool walkable = !obstacleData.obstacleGrid[index];
                // Create a new Node object for each cell in the grid, using the corresponding value from the obstacleGrid to determine if it is walkable or not
                grid[x, y] = new PathNode(x, y, walkable);
            }
        }
    }

    public List<PathNode> FindPath(Vector2Int startPos, Vector2Int targetPos)
    {
        // Check if the start and target positions are within the bounds of the grid
        if (!gridManager.IsWithinBounds(startPos) || !gridManager.IsWithinBounds(targetPos))
            return null;

        PathNode startNode = grid[startPos.x, startPos.y];
        PathNode targetNode = grid[targetPos.x, targetPos.y];
        List<PathNode> openList = new List<PathNode>();
        HashSet<PathNode> closedList = new HashSet<PathNode>();

        // Add the starting node to the open list
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                // Compare the fcost of the current node with the fcost of the nodes in the open list, and if they are equal,
                // Compare their hCost to determine which node to select as the current node
                if (openList[i].fcost < currentNode.fcost || (openList[i].fcost == currentNode.fcost && openList[i].hCost < currentNode.hCost))
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            if (currentNode == targetNode)
            {
                // If the target node is reached, retrace the path from the target node back to the start node and return it as a list of nodes
                return RetracePath(startNode, targetNode);
            }
            foreach (PathNode neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedList.Contains(neighbor))
                {
                    // If the neighbor node is not walkable or is already in the closed list, skip it
                    continue;
                }
                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }
        return null; // Return null if no path is found
    }

    // Retrace the path from the target node back to the start node by following the parent nodes, and return it as a list of nodes
    List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    // Get the neighboring nodes of a given node by checking the adjacent cells in the grid (up, down, left, right) and returning a list of valid neighbors
    List<PathNode> GetNeighbors(PathNode node)
    {
        List<PathNode> neighbors = new List<PathNode>();
        int[,] directions = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } }; // Up, Down, Left, Right
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int checkX = node.x + directions[i, 0];
            int checkY = node.y + directions[i, 1];
            if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
            {
                neighbors.Add(grid[checkX, checkY]);
            }
        }
        return neighbors;
    }

    // Calculate the distance between two nodes using the Manhattan distance formula, which is the sum of the absolute differences of their x and y coordinates
    int GetDistance(PathNode nodeA, PathNode nodeB)
    {
        int dstX = Mathf.Abs(nodeA.x - nodeB.x);
        int dstY = Mathf.Abs(nodeA.y - nodeB.y);
        return dstX + dstY;
    }
}
