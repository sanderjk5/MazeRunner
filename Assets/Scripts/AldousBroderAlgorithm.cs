using System.Collections;
using System.Collections.Generic;
using static System.Random;
using UnityEngine;

public class AldousBroderAlgorithm : MonoBehaviour
{
    private bool[] visitedNodes;
    private int numberOfVisitedNodes;
    public GameObject nodePrefab;
    public GameObject edgePrefab;

    /**
    * Initializes the static variables and calls the generation of the maze and the obstacles.
    * @param size of the maze (number of nodes = size^2)
    * @param numberOfObstacles of the maze
    */
    //public AldousBroderAlgorithm(int size, int numberOfObstacles)
    //{
    //Compute the values of each static variable of the main class.
    //Main.numberOfStates = (int)Math.pow(2, numberOfObstacles);
    //Main.numberOfButtons = numberOfObstacles;
    //Main.width = size;
    //Main.height = size;
    //Main.numberOfNodes = Main.width * Main.height;
    //visitedNodes = new boolean[Main.numberOfNodes];
    //numberOfVisitedNodes = 0;
    //Initializes the array and creates all nodes.
    //initializeVisitedNodesArray();
    //generateNodes();
    //Calls the actual Aldous Broder Algorithm.
    //generateMaze();
    //Main.numberOfEdges = Main.allEdges.size();
    //Generates a specific number of obstacles and buttons.
    //GenerateObstacles obstacleGenerator = new GenerateObstacles(numberOfObstacles);
    //Console.writeLine("hello");
    //}

    public void Initialize(int Width, int Height, int NumberOfObstacles)
    {
        // test

        //main part
        MainScript.NumberOfStates = 5;
        MainScript.NumberOfButtons = NumberOfObstacles;
        MainScript.Width = Width;
        MainScript.Height = Height;
        MainScript.NumberOfNodes = Width * Height;
        visitedNodes = new bool[MainScript.NumberOfNodes];
        numberOfVisitedNodes = 0;
        generateNodes();

        generateMaze();
        MainScript.NumberOfEdges = MainScript.AllEdges.Count;
        //TODO:: Add obstacle generation
    }

    /**
     * Creates every node and sets the neighbours for each of them.
     */
    private void generateNodes()
    {
        //initialize all nodes
        NodeController node;
        int nodecounter = 0;
        for (int i = 0; i < MainScript.Width; i++)
        {
            //because we start in the top left and iterate over each column from top to bottom we have to negate j
            for(int j = 0; j > -MainScript.Height; j--)
            {
                node = Instantiate(nodePrefab, new Vector3(i - 8.5f, j + 4.5f, 0), Quaternion.identity).GetComponent<NodeController>();
                node.Initialize(nodecounter, null, -1);
                MainScript.AllNodes.Add(nodecounter, node);
                nodecounter++;
            }
        }
        //initialize the neighbours list of each node
        for (int i = 0; i < MainScript.NumberOfNodes; i++)
        {
            node = MainScript.AllNodes[i];
            List<NodeController> neighbours = new List<NodeController>();

            if (i == 0)
            {
                neighbours.Add(MainScript.AllNodes[MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[1]);
            }
            else if (i == MainScript.Height - 1)
            {
                neighbours.Add(MainScript.AllNodes[i+ MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i-1]);
            }
            else if (i == MainScript.Height * (MainScript.Width - 1))
            {
                neighbours.Add(MainScript.AllNodes[i - MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i + 1]);             
            }
            else if (i == MainScript.NumberOfNodes - 1)
            {
                neighbours.Add(MainScript.AllNodes[i - MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i - 1]);
            }
            else if (0 < i && i < MainScript.Height - 1)
            {
                neighbours.Add(MainScript.AllNodes[i + MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i - 1]);        
                neighbours.Add(MainScript.AllNodes[i + 1]);               
            }
            else if (MainScript.Height * (MainScript.Width - 1) < i && i < MainScript.NumberOfNodes - 1)
            {
                neighbours.Add(MainScript.AllNodes[i - MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i - 1]);
                neighbours.Add(MainScript.AllNodes[i + 1]);
            }
            else if (i % MainScript.Height == 0)
            {
                neighbours.Add(MainScript.AllNodes[i + MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i - MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i + 1]);           
            }
            else if (i % MainScript.Height == MainScript.Height - 1)
            {
                neighbours.Add(MainScript.AllNodes[i + MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i - MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i - 1]);
            }
            else //for every of the "none border" nodes
            {
                neighbours.Add(MainScript.AllNodes[i + MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i - MainScript.Height]);
                neighbours.Add(MainScript.AllNodes[i + 1]);
                neighbours.Add(MainScript.AllNodes[i - 1]);
            }
            node.Neighbours = neighbours;
        }
    }

    /**
     * Retrieves a random node as start node for the algorithm.
     * @return the start node.
     */
    private NodeController getRandomStartNode()
    {
        System.Random rnd = new System.Random();
        int randomNumber = rnd.Next(0, MainScript.NumberOfNodes);
        return MainScript.AllNodes[randomNumber];
    }

    /**
     * Retrieves a random neighbour of a specific node.
     * @param node the given node.
     * @return a random neighbour.
     */
    private NodeController getRandomNeighbour(NodeController node)
    {
        List<NodeController> neighbours = node.Neighbours;
        System.Random rnd = new System.Random();
        int randomNumber = rnd.Next(0, neighbours.Count);
        return neighbours[randomNumber];
    }

    private KeyValuePair<Vector3, bool> getEdgeCoords(NodeController node1, NodeController node2)
    {
        Vector3 vec1 = node1.GetComponent<NodeController>().transform.position;
        Vector3 vec2 = node2.GetComponent<NodeController>().transform.position;
        Vector3 edgeCoords = Vector3.zero;
        KeyValuePair<Vector3, bool> pair;
        if (vec1.y == vec2.y) //this means that the edge has to be vertical
        {
            edgeCoords.x = (vec1.x + vec2.x) / 2;
            edgeCoords.y = vec1.y;
            pair = new KeyValuePair<Vector3, bool>(edgeCoords, true);
        }
        else // this means that the edge has to be horizontal
        {
            edgeCoords.x = vec1.x;
            edgeCoords.y = (vec1.y + vec2.y) / 2;
            pair = new KeyValuePair<Vector3, bool>(edgeCoords, false);
        }
        return pair;
    }

    /**
     * Computes the Aldous Broder Algorithm to generate a random maze.
     */
    private void generateMaze()
    {
        //Retrieves a random node as start node.
        NodeController currentNode = getRandomStartNode();
        visitedNodes[currentNode.Id] = true;
        numberOfVisitedNodes += 1;
        while (numberOfVisitedNodes < MainScript.NumberOfNodes)
        {
            NodeController nextNode = getRandomNeighbour(currentNode);
            if (!visitedNodes[nextNode.Id])
            {
                visitedNodes[nextNode.Id] = true;
                numberOfVisitedNodes += 1;
                EdgeController edge;

                KeyValuePair<Vector3, bool> coordsPair = getEdgeCoords(currentNode, nextNode);
                if (coordsPair.Value)  // edge has to be vertical
                {
                    edge = Instantiate(edgePrefab, coordsPair.Key, Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
                }
                else // edge has to be horizontal
                {
                    edge = Instantiate(edgePrefab, coordsPair.Key, Quaternion.identity).GetComponent<EdgeController>();
                }
                //Initialize the edge between the nodes
                edge.Initialize(currentNode, nextNode, null, -1);
                currentNode.OutgoingEdges.Add(edge);
                nextNode.OutgoingEdges.Add(edge);
                MainScript.AllEdges.Add(edge);
            }
            //Sets the neighbour as current node.
            currentNode = nextNode;
        }
        //TODO:: Add Timer
    }
}
