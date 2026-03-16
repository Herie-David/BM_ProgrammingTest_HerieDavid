using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PathFinding pathFinder;
    public Grid gridManager;

    public float moveSpeed = 5f;
    bool isMoving = false;
    public EnemyAI enemy;

    void Start()
    {
        // Set the player's starting position to (9, 5) in grid coordinates
        Vector3 startWorldPos = gridManager.GridToWorld(9, 5);
        transform.position = startWorldPos;
    }
    void Update()
    {
        // Handle mouse input for movement
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Convert the clicked world position to grid coordinates
                Vector2Int targetGridPos = gridManager.WorldToGrid(hit.point);
                Vector2Int currentGridPos = gridManager.WorldToGrid(transform.position);
                List<PathNode> path = pathFinder.FindPath(currentGridPos, targetGridPos);
                if (path != null && path.Count > 0)
                {
                    StartCoroutine(MoveAlongPath(path));
                }
            }
        }

    }

    IEnumerator MoveAlongPath(List<PathNode> path)
    {
        isMoving = true;
        foreach (PathNode node in path)
        {
            // Move towards the world position of the current node
            Vector3 targetWorldPos = gridManager.GridToWorld(node.x, node.y);
            while (Vector3.Distance(transform.position, targetWorldPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetWorldPos; // Ensure we snap to the exact position
        }
        isMoving = false;

        // After reaching the destination, execute the enemy's AI
        if (enemy != null)
        {
            enemy.ExecuteAI();
        }
    }
}