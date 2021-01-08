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


    // Start is called before the first frame update
    void Start()
    {
        NumberOfNodes = 0;
        NumberofEdges = 0;
        NumberofStates = 0;
        AllNodes = new Dictionary<int, NodeController>();
        AllEdges = new List<EdgeController>();
    }
}
