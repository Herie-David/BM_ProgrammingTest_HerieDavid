using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // A reference to the Obstacle ScriptableObject that contains the grid data for the obstacles.
    public Obstacle obstacleData;
    public GameObject obstaclePrefab;

    public float tilesize = 1f;

    void Start()
    {
        GenerateObstacle();
    }
    
    void GenerateObstacle()
    {
        for (int y =0; y < obstacleData.height; y++)
        {
            int index = 0;
            for (int x = 0; x < obstacleData.width; x++)
            {
                // Calculate the index in the obstacle grid based on the current x and y coordinates.
                index = y * obstacleData.width + x;
                if (obstacleData.obstacleGrid[index])
                {
                    // If the current cell in the obstacle grid is true, instantiate an obstacle prefab at the corresponding position.
                    Vector3 position = new Vector3(x * tilesize, +0.4f, y * tilesize);
                    Instantiate(obstaclePrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}
