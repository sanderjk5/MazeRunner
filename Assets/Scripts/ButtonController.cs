using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //The corresponding obstacle of the button.
    public EdgeController CorrespondingEdge { get; private set; }
    //The node on which the button is placed.
    public NodeController CorrespondingNode { get; private set; }

    /**
     * <summary>Initializes the button.</summary>
     * <param name="correspondingEdge">The corresponding edge.</param>
     * <param name="correspondingNode">The corresponding node.</param>
     * <param name="buttonId">The id of the button.</param>
     */
    public void Initialize(EdgeController correspondingEdge, NodeController correspondingNode, int buttonId)
    {
        CorrespondingEdge = correspondingEdge;
        CorrespondingNode = correspondingNode;
        //Sets the color of the button (equal to the color of the corresponding obstacle.
        gameObject.GetComponent<SpriteRenderer>().color = MainScript.Colors[buttonId];
    }

    /**
     * <summary>Changes the state of the game when the player activates the button. Changes the color of the corresponding obstacle.</summary>
     * <param name="other">The player which activates the button.</param>
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        MainScript.CurrentState = CorrespondingNode.ChangeState(MainScript.CurrentState);
        CorrespondingEdge.ChangeColorOfObstacle(MainScript.CurrentState);
    }
}
