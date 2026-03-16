# A* Pathfinding Implementation

This project implements an A* grid-based pathfinding system.

## Key Features
- **Grid Initialization:** Created a `PathNode` grid initialized from obstacle data.
- **A*** **Algorithm:** Implemented A* search algorithm utilizing open and closed lists.
- **Heuristic:** Added Manhattan distance heuristic designed for 4-directional movement.
- **Neighbor Detection:** Implemented neighbor detection for adjacent nodes (up, down, left, right).
- **Path Retracing:** Added functionality for path retracing from the target node back to the start node.
- **Obstacle Integration:** Integrated an obstacle grid to accurately determine walkable tiles.
- **Bounds Checking:** Added grid bounds checking via the Grid manager.

## Limitations
- The current implementation is optimized for small grids.
- Movement is restricted to non-diagonal (4-directional) movement.
