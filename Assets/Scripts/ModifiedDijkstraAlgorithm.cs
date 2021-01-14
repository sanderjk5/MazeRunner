using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class ModifiedDijkstraAlgorithm : MonoBehaviour
{
    // Fields
    private NodeController startNode;
    private NodeController endNode;
    private SimplePriorityQueue<HelperNodeController> prioQueue;
    private int[,] allDistances;

    // Properties
    private int ShortestDistance { get; }
    private List<NodeController> ShortestPath { get; }

    // Constructor which needs a start node and an end node for Dijkstra's algorithm
    public ModifiedDijkstraAlgorithm(NodeController node0, NodeController node1)
    {
        startNode = node0;
        endNode = node1;
        prioQueue = new SimplePriorityQueue<HelperNodeController>();
        InitializeAllDistances();
        ShortestDistance = 0;
        ShortestPath = new List<NodeController>();
    }

    // Initialize all distances of the nodes
    private void InitializeAllDistances()
    {
        allDistances = new int[MainScript.AllNodes.Count, MainScript.NumberOfNodes];
        for(int i = 0; i < MainScript.AllNodes.Count; i++)
        {
            for(int j = 0; j < MainScript.NumberofStates; j++)
            {
                allDistances[i, j] = int.MaxValue;
            }
        }
    }
}
