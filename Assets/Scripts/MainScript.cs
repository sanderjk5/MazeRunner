using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    //The width of the maze.
    public static int Width { get; set; }
    //The height of the maze.
    public static int Height { get; set; }
    //The number of nodes of the maze (width * height).
    public static int NumberOfNodes { get; set; }
    //The number of edges of the maze.
    public static int NumberOfEdges { get; set; }
    //The number of states.
    public static int NumberOfStates { get; set; }
    //The number of buttons.
    public static int NumberOfButtons { get; set; }
    //Contains all nodes of the maze.
    public static Dictionary<int, NodeController> AllNodes { get; set; }
    //Contains all edges of the maze.
    public static List<EdgeController> AllEdges { get; set; }

    //The prefab of the walls.
    public GameObject createWallsPrefab;
    //The prefab of the nodes.
    public GameObject nodePrefab;
    //The prefab of the edges.
    public GameObject edgePrefab;
    public GameObject aldousBroderAlgorithmPrefab;
    // Start is called before the first frame update. Calls the MazeGeneration and the CreateAllWalls method.
    void Start()
    {
        
        //Initializes the static variables of the game.
        NumberOfNodes = 0;
        NumberOfEdges = 0;
        NumberOfStates = 0;
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();

        AldousBroderAlgorithm a = Instantiate(aldousBroderAlgorithmPrefab).GetComponent<AldousBroderAlgorithm>();
        a.Initialize(18, 10, 0);

        //this.CreateFakeData();
        //Creates all walls of the maze.
        GameObject createWallsObject = Instantiate(createWallsPrefab);
        CreateWalls createWallsScript = createWallsObject.GetComponent<CreateWalls>();
        createWallsScript.CreateAllWalls();
    }

    /**
     * An example of creating a small maze.
     */
    private void CreateFakeData()
    {
        //Sets the variables.
        NumberOfNodes = 4;
        NumberOfEdges = 3;
        NumberOfStates = 1;
        NumberOfButtons = 0;
        Width = 2;
        Height = 2;

        //Creates all nodes.
        NodeController node0 = Instantiate(nodePrefab, new Vector3(-0.5f, 0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node0.Initialize(0, null, -1);
        AllNodes.Add(0, node0);

        NodeController node1 = Instantiate(nodePrefab, new Vector3(-0.5f, -0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node1.Initialize(1, null, -1);
        AllNodes.Add(1, node1);

        NodeController node2 = Instantiate(nodePrefab, new Vector3(0.5f, 0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node2.Initialize(2, null, -1);
        AllNodes.Add(2, node2);

        NodeController node3 = Instantiate(nodePrefab, new Vector3(0.5f, -0.5f, -1), Quaternion.identity).GetComponent<NodeController>();
        node3.Initialize(3, null, -1);
        AllNodes.Add(3, node3);

        //Creates all edges.
        EdgeController edge0 = Instantiate(edgePrefab, new Vector3(0, 0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge0.Initialize(node0, node2, null, -1);
        node0.OutgoingEdges.Add(edge0);
        node2.OutgoingEdges.Add(edge0);
        AllEdges.Add(edge0);

        EdgeController edge1 = Instantiate(edgePrefab, new Vector3(0.5f, 0, -1), Quaternion.identity).GetComponent<EdgeController>();
        edge1.Initialize(node2, node3, null, -1);
        node2.OutgoingEdges.Add(edge1);
        node3.OutgoingEdges.Add(edge1);
        AllEdges.Add(edge1);

        EdgeController edge2 = Instantiate(edgePrefab, new Vector3(0, -0.5f, -1), Quaternion.Euler(0, 0, 90)).GetComponent<EdgeController>();
        edge2.Initialize(node1, node3, null, -1);
        node1.OutgoingEdges.Add(edge2);
        node3.OutgoingEdges.Add(edge2);
        AllEdges.Add(edge2);
    }
}
