using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseHover : MonoBehaviour
{
    // Reference to the Text component to display tile information
    public TMP_Text tileText;
    Tile currentTile; // Store the currently highlighted tile

    void Update()
    {
        // Perform a raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has a Tile component
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                // Display tile information in the Text component
                tileText.text = $"Tile: ({tile.GridPosition})";
                // Highlight the tile if it's not already highlighted
                if (currentTile != tile)
                {
                    if (currentTile != null)
                    {
                        currentTile.ResetColor(); // Reset the previously highlighted tile
                    }
                    tile.Highlight(); // Highlight the new tile
                    currentTile = tile; // Update the current tile reference
                }

            return;
            }
        }

        // If the raycast does not hit a tile, clear the text and reset any highlighted tile
        if (currentTile != null)
        {
            currentTile.ResetColor();
            currentTile = null;
        }
    }
}
