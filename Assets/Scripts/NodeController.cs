using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public int Id { get; private set; }
    public int[] States { get; set; }
    public List<EdgeController> OutgoingEdges { get; private set; }
    public int Button { get; set; }
    public List<NodeController> Neighbours { get; set; }

    public void Initialize(int id, int[] states, int button)
    {
        Id = id;
        States = states;
        OutgoingEdges = new List<EdgeController>();
        Neighbours = new List<NodeController>();
        Button = button;
    }

    public int ChangeState(int state)
    {
        if (Button != -1) return States[state];
        return state;
    }

    public EdgeController GetEdgeToNode(NodeController targetNode)
    {
        foreach(EdgeController edge in OutgoingEdges)
        {
            if(edge.Node0.Id == targetNode.Id || edge.Node1.Id == targetNode.Id) return edge;
        }
        return null;
    }
}
