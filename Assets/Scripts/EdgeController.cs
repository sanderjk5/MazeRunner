using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    //The two nodes which are connected by the edge.
    public NodeController Node0 { get; private set; }
    public NodeController Node1 { get; private set; }
    //The costs of the edge (depend on the current state)
    public int[] Costs { get;  set; }
    //Id of the obstacle (-1 if the edge is not an obstacle).
    public int Obstacle { get; set; }

    /**
     * Sets all variables of the edge.
     */
    public void Initialize(NodeController node0, NodeController node1, int[] costs, int obstacle)
    {
        Node0 = node0;
        Node1 = node1;
        Costs = costs;
        Obstacle = obstacle;
    }

    // Gets the cost for the respective state
    public int GetCostForState(int state)
    {
        if (Obstacle != -1) return Costs[state];
        return 1;
    }
}
