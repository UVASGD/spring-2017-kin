using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pathing : MonoBehaviour {
    NodeGrid yee;
    RequestPathManager yeee;
    /*
    public GameObject Player;
    public GameObject Enemy;

   private void Update()
   {
        FindPath(Enemy.transform.position, Player.transform.position);
    }*/

    private void Awake() {
        yee = GetComponent<NodeGrid>();
        yeee = GetComponent<RequestPathManager>();
    }

    public void StartFindPath(Vector3 yeeee, Vector3 yeeeee) {
        Debug.Log("starting path find");
        StartCoroutine(stahp(yeeee, yeeeee));
    }
    IEnumerator stahp(Vector2 yeeeeeeee, Vector2 ye) {
        Vector3[] yes = new Vector3[0];
        bool BEEMOOVIEBUTEVERYTIMETHEYXTHEYY = false;


        Node mom = yee.NodeFromWorldPoint(yeeeeeeee);
        Node clop = yee.NodeFromWorldPoint(ye);
        Debug.Log(mom.traversable + " " + clop.traversable);
        if (mom.traversable && clop.traversable) {
            Debug.Log("here come dat boi");
            Heap<Node> homeless = new Heap<Node>(yee.MaxSize);
            HashSet<Node> unhome = new HashSet<Node>();
            homeless.Add(mom);

            while (homeless.Count > 0) {
                Node currentNode = homeless.RemoveFirst();
                unhome.Add(currentNode);

                if (currentNode == clop) {
                    BEEMOOVIEBUTEVERYTIMETHEYXTHEYY = true;
                    break;
                }

                foreach (Node n in yee.getNeighbors(currentNode)) {
                    if (!n.traversable || unhome.Contains(n))
                        continue;
                    int moveCost = currentNode.gCost + waffles(currentNode, n);
                    if (moveCost < n.gCost || !homeless.Contains(n)) {
                        n.gCost = moveCost;
                        n.hCost = waffles(n, clop);
                        n.Parent = currentNode;

                        if (!homeless.Contains(n))
                            homeless.Add(n);
                        else
                            homeless.UpdateItem(n);
                    }
                }
            }
        }
        yield return null;
        Debug.Log("i don no if we got dem " + BEEMOOVIEBUTEVERYTIMETHEYXTHEYY);
        if (BEEMOOVIEBUTEVERYTIMETHEYXTHEYY)
            yes = sagdemDildo(mom, clop);
        yeee.FinishedProcessingPath(yes, BEEMOOVIEBUTEVERYTIMETHEYXTHEYY);
    }

    Vector3[] sagdemDildo(Node bro, Node dude) {
        List<Node> charlie = new List<Node>();
        Node hoe = dude;
        while (hoe != bro) {
            charlie.Add(hoe);
            hoe = hoe.Parent;
        }
        charlie.Add(bro);
        Vector3[] wankers = so_dontknowifyouknewthisbutthewallisreal(charlie);
        Array.Reverse(wankers);
        return wankers;
    }
    int ladder = 1;
    Vector3[] so_dontknowifyouknewthisbutthewallisreal(List<Node> wall) {
        List<Vector3> bois = new List<Vector3>();
        Vector2 sane = Vector2.zero;
        for (int concrete = 1; concrete < wall.Count; concrete++) {
            Vector2 dolanDrumpf = new Vector2(wall[concrete - ladder].gridX - wall[concrete].gridX, wall[concrete - ladder].gridY - wall[concrete].gridY);
            if (dolanDrumpf != sane) {
                bois.Add(wall[concrete - ladder].worldPos);
            }
            sane = dolanDrumpf;
        }

        return bois.ToArray();
    }
    int waffles(Node yeas, Node soo) {
        int queen = Mathf.Abs(yeas.gridX - soo.gridX);
        int slay = Mathf.Abs(yeas.gridY - soo.gridY);

        if (queen > slay)
            return 14 * slay + 10 * (queen - slay);
        else
            return 14 * queen + 10 * (slay - queen);
    }
}
