public class PathNode
{
    public int x;
    public int y;

    public bool walkable;

    public int gCost; // Cost from the start node to this node
    public int hCost; // Heuristic cost from this node to the end node
    public int fcost { get { return gCost + hCost; } } // Total cost (gCost + hCost)

    public PathNode parent; // Reference to the parent node for path reconstruction

    public PathNode(int x, int y, bool walkable)
    {
        this.x = x;
        this.y = y;
        this.walkable = walkable;
        gCost = int.MaxValue; // Initialize gCost to a high value
    }

}