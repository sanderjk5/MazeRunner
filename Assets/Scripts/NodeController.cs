using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    //The Id of the node.
    public int Id { get; private set; }
    //The state transition array.
    public int[] States { get; set; }
    //All outgoing edges of the node.
    public List<EdgeController> OutgoingEdges { get; private set; }
    //The id of the button (-1 if the node is not a button)
    public int Button { get; set; }
    //All neighbour nodes.
    public List<NodeController> Neighbours { get; set; }

    /**
     * Sets all variables of the node.
     */
    public void Initialize(int id, int[] states, int button)
    {
        Id = id;
        States = states;
        OutgoingEdges = new List<EdgeController>();
        Neighbours = new List<NodeController>();
        Button = button;
    }

    /**
     * Gets the new state respective to the current state.
     */
    public int ChangeState(int state)
    {
        if (Button != -1) return States[state];
        return state;
    }

    /**
     * Gets the edge to the given node.
     */
    public EdgeController GetEdgeToNode(NodeController targetNode)
    {
        foreach(EdgeController edge in OutgoingEdges)
        {
            if(edge.Node0.Id == targetNode.Id || edge.Node1.Id == targetNode.Id) return edge;
        }
        return null;
    }

    /**
     * Store the node if the player stepped on it.
     */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (MainScript.IsBattleGameMode && GameObject.Find("Opponent").GetComponent<BoxCollider2D>().Equals(collision))
        {
            return;
        }

        if (MainScript.PlayerPath.Count != 0)
        {
            NodeController preNode = MainScript.PlayerPath[MainScript.PlayerPath.Count - 1];
            IEnumerable<EdgeController> edgeIntersect = this.OutgoingEdges.Intersect(preNode.OutgoingEdges);
            if (!this.Neighbours.Contains(preNode))
            {
                IEnumerable<NodeController> intersect = preNode.Neighbours.Intersect(this.Neighbours);

                foreach (NodeController i in intersect)
                {
                    IEnumerable<EdgeController> edgeIntersectPreNode = i.OutgoingEdges.Intersect(preNode.OutgoingEdges);
                    IEnumerable<EdgeController> edgeIntersectCurrentNode = i.OutgoingEdges.Intersect(this.OutgoingEdges);
                    if (edgeIntersectPreNode.Count() != 0 && edgeIntersectCurrentNode.Count() != 0)
                    {
                        MainScript.CurrentStepCount += edgeIntersectPreNode.ElementAt(0).GetCostForState(MainScript.CurrentState);
                        MainScript.PlayerPath.Add(i);
                        MainScript.CurrentStepCount += edgeIntersectCurrentNode.ElementAt(0).GetCostForState(MainScript.CurrentState);
                        MainScript.PlayerPath.Add(this);
                        break;
                    }
                }
            }
            else if(edgeIntersect.Count() == 0)
            {
                foreach(NodeController neighbourOfPreNode in preNode.Neighbours)
                {
                    IEnumerable<NodeController> intersectsOfNeighbourOfPreNode = neighbourOfPreNode.Neighbours.Intersect(this.Neighbours);
                    foreach (NodeController i in intersectsOfNeighbourOfPreNode)
                    {
                        IEnumerable<EdgeController> edgeIntersectPreNode = preNode.OutgoingEdges.Intersect(neighbourOfPreNode.OutgoingEdges);
                        IEnumerable<EdgeController> edgeIntersectNeighbourOfPreNode= neighbourOfPreNode.OutgoingEdges.Intersect(i.OutgoingEdges);
                        IEnumerable<EdgeController> edgeIntersectCurrentNode = this.OutgoingEdges.Intersect(i.OutgoingEdges);
                        if (edgeIntersectPreNode.Count() != 0 && edgeIntersectNeighbourOfPreNode.Count() != 0 && edgeIntersectCurrentNode.Count() != 0)
                        {
                            MainScript.CurrentStepCount += edgeIntersectPreNode.ElementAt(0).GetCostForState(MainScript.CurrentState);
                            MainScript.PlayerPath.Add(neighbourOfPreNode);
                            MainScript.CurrentStepCount += edgeIntersectNeighbourOfPreNode.ElementAt(0).GetCostForState(MainScript.CurrentState);
                            MainScript.PlayerPath.Add(i);
                            MainScript.CurrentStepCount += edgeIntersectCurrentNode.ElementAt(0).GetCostForState(MainScript.CurrentState);
                            MainScript.PlayerPath.Add(this);
                            break;
                        }
                    }
                }
            }
            else {
                MainScript.CurrentStepCount += edgeIntersect.ElementAt(0).GetCostForState(MainScript.CurrentState);
                MainScript.PlayerPath.Add(this);
            }
        }
        else
        {
            MainScript.PlayerPath.Add(this);
        }
        MainScript.UpdateStepCounter();
    }

    /**
     * <summary>Activates the button again if the player leaves the corresponding node.</summary>
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(Button != -1)
        {
            MainScript.AllButtons[Button].IsActivated = true;
        }
    }
}
