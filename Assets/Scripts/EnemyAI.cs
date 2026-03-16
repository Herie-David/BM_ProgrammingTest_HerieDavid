using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public Transform player;
    public float speed = 5f;

    public Grid gridManger;
    public PathFinding pathFinding;

    bool isMoving = false;

    Vector2Int lasttargetPlayerPos;

    void Start()
    {
        Initialize();
    }

    // Initialize the enemy's position and store the player's initial grid position
    public void Initialize()
    {
        Vector3 startWorldPos = gridManger.GridToWorld(9, 9);
        transform.position = startWorldPos;

        lasttargetPlayerPos = gridManger.WorldToGrid(player.position);
    }
    public void ExecuteAI()
    {
        // Check if the player is still valid and if the enemy is not currently moving
        if (player != null && !isMoving)
        {
            Vector2Int targetPlayerPos = gridManger.WorldToGrid(player.position);
            if (targetPlayerPos == lasttargetPlayerPos)
            {
                return; // No need to recalculate path if player hasn't moved
            }
            lasttargetPlayerPos = targetPlayerPos;
            Vector2Int currentEnemyPos = gridManger.WorldToGrid(transform.position);

            Vector2Int[] adjacentTiles = 
            {   targetPlayerPos + Vector2Int.up, 
                targetPlayerPos + Vector2Int.down, 
                targetPlayerPos + Vector2Int.left, 
                targetPlayerPos + Vector2Int.right 
            };

            List <PathNode> bestpath = null;
            foreach (Vector2Int tile in adjacentTiles)
            {
                if (!gridManger.IsWithinBounds(tile))
                {
                    continue; // Skip tiles that are out of bounds
                }

                List<PathNode> path = pathFinding.FindPath(currentEnemyPos, tile);
                if (path != null && (bestpath == null || path.Count < bestpath.Count))
                {
                    bestpath = path;
                }
            }
            // If a valid path to any of the adjacent tiles is found, move along the best path
            if (bestpath != null)
            {
                StartCoroutine(MoveAlongPath(bestpath));
            }

        }
    }

    // Coroutine to move the enemy along the calculated path
    IEnumerator MoveAlongPath(List<PathNode> path)
    {
        isMoving = true;
        foreach (PathNode node in path)
        {
            Vector3 targetWorldPos = gridManger.GridToWorld(node.x, node.y);
            while (Vector3.Distance(transform.position, targetWorldPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, speed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetWorldPos; // Ensure we snap to the exact position
        }
        isMoving = false;
    }
}