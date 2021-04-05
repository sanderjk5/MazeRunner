using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    //The two nodes which are connected by the edge.
    public NodeController Node0 { get; private set; }
    public NodeController Node1 { get; private set; }
    //The costs of the edge (depend on the current state)
    public int[] Costs { get;  set; }
    //Id of the obstacle (-1 if the edge is not an obstacle).
    public int Obstacle { get; set; }

    /**
     * Sets all variables of the edge.
     */
    public void Initialize(NodeController node0, NodeController node1, int[] costs, int obstacle)
    {
        Node0 = node0;
        Node1 = node1;
        Costs = costs;
        Obstacle = obstacle;
    }

    // Gets the cost for the respective state
    public int GetCostForState(int state)
    {
        if (Obstacle != -1) return Costs[state];
        return 1;
    }

    /**
     * <summary>Changes the color of the obstacle. Sets the transparency to zero if the player deactivates it (by activating the button).</summary>
     * <param name="newState">The current state of the game.</param>
     */
    public void ChangeColorOfObstacle(int newState)
    {
        if(Obstacle != -1 && Costs[newState] != 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = MainScript.Colors[Obstacle];
            //Changes the width of the edge/obstacle.
            if (MainScript.ScaleMazeSize == 0.5)
            {
                gameObject.GetComponent<Transform>().localScale = new Vector3(MainScript.ScaleMazeSize, 0.025f);
            }
            else
            {
                gameObject.GetComponent<Transform>().localScale = new Vector3(MainScript.ScaleMazeSize, 0.03f);
            }
            
        } else
        {
            //Sets the transparency to zero and changes the width of the obstacle.
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            gameObject.GetComponent<Transform>().localScale = new Vector3(0.97f * MainScript.ScaleMazeSize, 0.01f);
        }
    }

    /**
     * <summary>Updates the step counter of the game if the player collides the edge.</summary>
     * <param name="collision">The collision which results in the update of the step counter.</param>
     * 
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.Find("Ruby").GetComponent<EdgeCollider2D>().Equals(collision))
        {
            MainScript.CurrentStepCount += GetCostForState(MainScript.CurrentState);
            MainScript.UpdateStepCounter();
        } else if (GameObject.Find("Opponent").GetComponent<BoxCollider2D>().Equals(collision))
        {
            GameObject.Find("Opponent").GetComponent<OpponentController>().StepCounter += GetCostForState(MainScript.CurrentState);
            GameObject.Find("Opponent").GetComponent<OpponentController>().UpdateStepCounter();
        }
        
    }
}
