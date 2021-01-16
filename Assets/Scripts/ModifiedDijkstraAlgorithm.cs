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
    public int ShortestDistance { get; private set; }
    public ArrayList ShortestPath { get; private set; }

    // Constructor which needs a start node and an end node for Dijkstra's algorithm
    public ModifiedDijkstraAlgorithm(NodeController node0, NodeController node1)
    {
        startNode = node0;
        endNode = node1;
        prioQueue = new SimplePriorityQueue<HelperNodeController>();
        InitializeAllDistances();
    }

    // Initialize all distances of the nodes
    private void InitializeAllDistances()
    {
        allDistances = new int[MainScript.NumberOfNodes, MainScript.NumberofStates];
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
                // edge.Node1.Id ungleich currentNode.Id ansonsten edge.Node0.Id
                if(edge.Node1.Id != currentNode.Id)
                {
                    newNode.Initialize(edge.Node1.Id, newState, newDistance, currentHelperNode);
                }
                else
                {
                    newNode.Initialize(edge.Node0.Id, newState, newDistance, currentHelperNode);

                }
                if (DecideToAddNewNode(newNode)) prioQueue.Enqueue(newNode, newDistance);
            }
        }

        // Calculate the distance and path of the result
        if(endHelperNode != null)
        {
            ShortestDistance = endHelperNode.Distance;
            CalculatePathBetweenStartAndEndNode(endHelperNode);
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

    // Calculates the path between start and target
    private void CalculatePathBetweenStartAndEndNode(HelperNodeController endHelperNode)
    {
        ShortestPath = new ArrayList();
        ArrayList pathHelperList = new ArrayList();
        HelperNodeController currentHelperNode = endHelperNode;

        while(currentHelperNode != null)
        {
            pathHelperList.Add(MainScript.AllNodes[currentHelperNode.Id]);
            currentHelperNode = currentHelperNode.Predecessor;
        }

        for(int i = pathHelperList.Count - 1; i >= 0; i--)
        {
            ShortestPath.Add(pathHelperList[i]);
        }
    }
}
