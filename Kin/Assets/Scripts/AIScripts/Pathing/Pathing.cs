using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pathing : MonoBehaviour {
    NodeGrid grid;
    RequestPathManager requestManager;
    /*
    public GameObject Player;
    public GameObject Enemy;

   private void Update()
   {
        FindPath(Enemy.transform.position, Player.transform.position);
    }*/

    private void Awake()
    {
        grid = GetComponent<NodeGrid>();
        requestManager = GetComponent<RequestPathManager>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        //Debug.Log("starting path find");
        StartCoroutine(FindPath(startPos, targetPos));
    }
    IEnumerator FindPath(Vector2 start, Vector2 end)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;


        Node startNode = grid.NodeFromWorldPoint(start);
        Node targetNode = grid.NodeFromWorldPoint(end);
        //Debug.Log(startNode.traversable + " " + targetNode.traversable);
        if (startNode.traversable && targetNode.traversable)
        {
            //Debug.Log("both traversable");
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node n in grid.getNeighbors(currentNode))
                {
                    if (!n.traversable || closedSet.Contains(n))
                        continue;
                    int moveCost = currentNode.gCost + GetDistance(currentNode, n);
                    if (moveCost < n.gCost || !openSet.Contains(n))
                    {
                        n.gCost = moveCost;
                        n.hCost = GetDistance(n, targetNode);
                        n.Parent = currentNode;

                        if (!openSet.Contains(n))
                            openSet.Add(n);
                        else
                            openSet.UpdateItem(n);
                    }
                }
            }
        }
        yield return null;
        //ebug.Log("Checking path find success" + pathSuccess);
        if (pathSuccess)
            waypoints = tracePath(startNode, targetNode);
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] tracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;
        while (currentNode != start) {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Add(start);
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i-1].worldPos);
            }
            directionOld = directionNew;
        }

        return waypoints.ToArray();

    }

    int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        else
            return 14 * distX + 10 * (distY - distX);
        

    }
}
