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
    public GameObject helperNodePrefab;
    private ArrayList garbage = new ArrayList();
    private int initialState;

    // Properties
    public int ShortestDistance { get; private set; }
    public List<NodeController> ShortestPath { get; private set; }

    // Initializer which needs a start node and an end node for Dijkstra's algorithm
    public void Initialize(NodeController node0, NodeController node1)
    {
        startNode = node0;
        endNode = node1;
        prioQueue = new SimplePriorityQueue<HelperNodeController>();
        this.initialState = 0;
        InitializeAllDistances();
    }

    public void Initialize(NodeController node0, NodeController node1, int initialState)
    {
        startNode = node0;
        endNode = node1;
        this.initialState = initialState;
        prioQueue = new SimplePriorityQueue<HelperNodeController>();
        InitializeAllDistances();
    }

    // Initialize all distances of the nodes
    private void InitializeAllDistances()
    {
        allDistances = new int[MainScript.NumberOfNodes, MainScript.NumberOfStates];
        for(int i = 0; i < MainScript.AllNodes.Count; i++)
        {
            for(int j = 0; j < MainScript.NumberOfStates; j++)
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
        GameObject helpObject = Instantiate(helperNodePrefab);
        HelperNodeController help = helpObject.GetComponent<HelperNodeController>();
        help.Initialize(startNode.Id, initialState, 0, null);
        prioQueue.Enqueue(help, 0);
        garbage.Add(helpObject);
        allDistances[startNode.Id, initialState] = 0;

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
                GameObject newNodeObject = Instantiate(helperNodePrefab);
                HelperNodeController newNode = newNodeObject.GetComponent<HelperNodeController>();
                garbage.Add(newNodeObject);

                // Check which node is the target node
                if (edge.Node1.Id != currentNode.Id)
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

        // Destroy garbage
        foreach(GameObject obj in garbage)
        {
            Destroy(obj);
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
        ShortestPath = new List<NodeController>();
        List<NodeController> pathHelperList = new List<NodeController>();
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
