using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float tileSize = 1f;

    // Reference to the tile prefab to instantiate
    public GameObject tilePrefab;

    public Tile[,] grid;
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Instantiate a tile at the correct position
                Vector3 tilePosition = new Vector3(x * tileSize, 0, y * tileSize);
                GameObject tileObject = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tileObject.name = $"Tile_{x}_{y}";
                // Store the tile in the grid array
                Tile tile = tileObject.GetComponent<Tile>();
                
                if (tile != null)
                {
                    tile.gridX = x;
                    tile.gridY = y;
                    grid[x, y] = tile;
                }
            }
        }
    }

    // Convert world position to grid coordinates
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / tileSize);
        int y = Mathf.RoundToInt(worldPos.z / tileSize);
        return new Vector2Int(x, y);
    }

    // Convert grid coordinates to world position
    public Vector3 GridToWorld(int x, int y)
    { 
            return new Vector3(x * tileSize, 0.4f, y * tileSize);
    }

    // Check if the given grid position is within the bounds of the grid
    public bool IsWithinBounds(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < width && gridPos.y >= 0 && gridPos.y < height;
    }
}
