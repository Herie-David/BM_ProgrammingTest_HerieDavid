using UnityEngine;

// This script defines a ScriptableObject called Obstacle, which can be used to store data about obstacles in a grid-based system.
[CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable Objects/Obstacle Data")]
public class Obstacle : ScriptableObject
{
    public int width = 10;
    public int height = 10;

    // A 2D array representing the grid of the obstacle, where true indicates an obstacle and false indicates a free space.
    public bool[] obstacleGrid = new bool[100];

    private void OnEnable()
    {
        // Ensure the obstacle grid is initialized with the correct size based on width and height.
        if (obstacleGrid == null || obstacleGrid.Length != width * height)
        {
            obstacleGrid = new bool[width * height];
        }
    }
}
