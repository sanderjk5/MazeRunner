using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public static int Width { get; set; }
    public static int Height { get; set; }
    public static int NumberOfNodes { get; set; }
    public static int NumberofEdges { get; set; }
    public static int NumberofStates { get; set; }
    public static int NumberofButtons { get; set; }
    public static Dictionary<int, NodeController> AllNodes { get; set; }
    public static List<EdgeController> AllEdges { get; set; }

    public GameObject createWallsPrefab;
    public GameObject nodePrefab;
    public GameObject edgePrefab;

    // Start is called before the first frame update
    void Start()
    {
        NumberOfNodes = 0;
        NumberofEdges = 0;
        NumberofStates = 0;
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();

        this.CreateFakeData();

        GameObject createWallsObject = Instantiate(createWallsPrefab);
        CreateWalls createWallsScript = createWallsObject.GetComponent<CreateWalls>();
        createWallsScript.CreateAllWalls();
    }

    private void CreateFakeData()
    {
        NumberOfNodes = 4;
        NumberofEdges = 3;
        NumberofStates = 1;
        NumberofButtons = 0;
        Width = 2;
        Height = 2;

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
