using UnityEngine;
using UnityEditor;

public class ObstacleEditor : EditorWindow
{
    Obstacle data;
    // Add menu item named "Obstacle Editor" to the Tools menu
    [MenuItem("Tools/Obstacle Editor")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, make one.
        GetWindow<ObstacleEditor>("Obstacle Editor");
    }

    void OnGUI()
    {
        // Display a label and an object field to select the Obstacle ScriptableObject
        GUILayout.Label("Obstacle Editor", EditorStyles.boldLabel);
        data = (Obstacle)EditorGUILayout.ObjectField("Obstacle Data", data, typeof(Obstacle), false);
        if (data.obstacleGrid == null || data.obstacleGrid.Length != data.width * data.height)
        {
            data.obstacleGrid = new bool[data.width * data.height];
        }
        for (int y = 0; y < data.height; y++)
        {
            // Display a horizontal group of toggles for each row of the obstacle grid
            GUILayout.BeginHorizontal();
            int index = 0;
            for (int x = 0; x < data.width; x++)
            {
                // Calculate the index for the obstacle grid based on the current x and y, but rotate the y-axis so that the grid is displayed with (0,0) at the bottom left
                // To match the way it is seen in the game scene
                int rotatedY = data.height - 1 - y;
                index = rotatedY * data.width + x;
                bool currentValue = data.obstacleGrid[index];
                bool newValue = GUILayout.Toggle(currentValue, "", GUILayout.Width(16));
                if (newValue != currentValue)
                {
                    // Record the change for undo functionality and mark the data as dirty so that it will be saved
                    Undo.RecordObject(data, "Toggle Obstacle Grid");
                    data.obstacleGrid[index] = newValue;
                    EditorUtility.SetDirty(data);
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
