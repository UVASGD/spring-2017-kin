using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour {

    Node[,] grid;
    public Vector2 gridSize;
    public float nodeRadius;
    //public LayerMask untraversableMask;
    public GameObject player;
    public bool limitGizmos;

    int gridSizeX, gridSizeY;

    private void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridSize.x / (2 * nodeRadius));
        gridSizeY = Mathf.RoundToInt(gridSize.y / (2 * nodeRadius));
        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - new Vector3(gridSize.x / 2, gridSize.y / 2, 0);
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = bottomLeft + (Vector3.right * (2 * nodeRadius * i + nodeRadius)) + (Vector3.up * (2 * nodeRadius * j + nodeRadius));
                bool traversable = true;
                Collider2D[] collidersInNode = Physics2D.OverlapCircleAll(worldPoint, nodeRadius);
                foreach (Collider2D current in collidersInNode)
                {
                    traversable = !(!traversable || current.gameObject.tag == "Untraversable");
                }
                grid[i, j] = new Node(traversable, worldPoint,i,j);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPoint)
    {
        float percentX =  Mathf.Clamp01((worldPoint.x - transform.position.x) / gridSize.x + .5f - (nodeRadius / gridSize.x));
        float percentY = Mathf.Clamp01((worldPoint.y - transform.position.y) / gridSize.y + .5f - (nodeRadius / gridSize.y));
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
        
    }

    public List<Node> getNeighbors(Node n)
    {
        List<Node> neighbors = new List<Node>();
        for (int x = -1; x <=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int checkX = n.gridX + x;
                int checkY = n.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }

    public List<Node> path;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));
        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.transform.position);
            if (!limitGizmos)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.traversable) ? Color.white : Color.red;
                    if (playerNode == n)
                        Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(n.worldPos, Vector3.one * (2 * nodeRadius - .1f));

                }
            }
        }
    }
}
