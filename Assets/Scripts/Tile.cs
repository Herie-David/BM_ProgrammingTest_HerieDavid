using UnityEngine;

public class Tile : MonoBehaviour
{
    public int gridX;
    public int gridY;

    Renderer TileRenderer;
    Color originalColor;
    // This variable indicates whether the tile is blocked or not.
    // For default, we set it to false, meaning the all the tile in grid is walkable.
    public bool isBlocked = false;

    void Start()
    {
        TileRenderer = GetComponent<Renderer>();
        originalColor = TileRenderer.material.color;
    }

    public void Highlight()
    {
        // Change the tile's color to yellow to indicate that it is highlighted.
        TileRenderer.material.color = Color.yellow;
    }

    public void ResetColor()
    {
        // Reset the tile's color to its original color.
        TileRenderer.material.color = originalColor;
    }

    // This property allows you to easily access the tile's grid position as a Vector2Int.
    public Vector2Int GridPosition
        {
        get { return new Vector2Int(gridX, gridY); }
    }
}
