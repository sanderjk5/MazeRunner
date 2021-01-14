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

    // Performs the modified dijkstra algorithm and calls the method to fill the shortestPathBetweenStartAndEndNode list.
    public void CalculateModifiedDijkstraAlgorithm()
    {
        HelperNodeController endHelperNode = null;

        // Add the start node with state = 0 and distance = 0 to the SimplePriorityQueue
        HelperNodeController help = new HelperNodeController();
        help.Initialize(startNode.Id, 0, 0, null);
        prioQueue.Enqueue(help, 0);
        allDistances[startNode.Id, 0] = 0;

        while(prioQueue.Count != 0)
        {
            // Retrieve the node with the smallest distance
            HelperNodeController currentHelperNode = prioQueue.Dequeue();

            // Skip the node if it was treated yet
            if(currentHelperNode.Distance != allDistances[currentHelperNode.Id, currentHelperNode.State]) continue;

            NodeController currentNode = MainScript.AllNodes[currentHelperNode.Id];

            // End node test
            if(currentHelperNode.Id == endNode.Id)
            {
                endHelperNode = currentHelperNode;
                break;
            }

            // Perform the state change of the current node
            int newState = currentNode.ChangeState(currentHelperNode.State);

            // Handle all outgoing edges of the current node
            foreach(EdgeController edge in currentNode.OutgoingEdges)
            {
                // Calculate the new distance of the end node of the edge
                int newDistance = currentHelperNode.Distance + edge.GetCostForState(newState);

                // Create the new node and add it if allowed
                HelperNodeController newNode = new HelperNodeController();
                newNode.Initialize(edge.Node1.Id, newState, newDistance, currentHelperNode);
                if (DecideToAddNewNode(newNode)) prioQueue.Enqueue(newNode, newDistance);
            }
        }
    }

    // Checks whether to add the node to the queue or not
    private bool DecideToAddNewNode(HelperNodeController nodeController)
    {
        if(nodeController.Distance < allDistances[nodeController.Id, nodeController.State])
        {
            // Update smallest known distance
            allDistances[nodeController.Id, nodeController.State] = nodeController.Distance;
            return true;
        }
        return false;
    }
}
