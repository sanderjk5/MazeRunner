using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //The corresponding obstacle of the button.
    public EdgeController CorrespondingEdge { get; private set; }
    //The node on which the button is placed.
    public NodeController CorrespondingNode { get; private set; }
    //The id of the button.
    private int buttonId;
    //Flag if the button is activated.
    private bool _isActivated;

    public bool IsActivated
    {
        get => _isActivated;
        set
        {
            if(!value)
            {
                //Changes the transparency if the button gets deactivated.
                Color color = gameObject.GetComponent<SpriteRenderer>().color;
                color.a = 0.5f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
            }
            else
            {
                //Resets the transparency.
                gameObject.GetComponent<SpriteRenderer>().color = MainScript.Colors[buttonId];
            }
            _isActivated = value;
        }
    }

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
        this.buttonId = buttonId;

        //Sets the color of the button (equal to the color of the corresponding obstacle.
        gameObject.GetComponent<SpriteRenderer>().color = MainScript.Colors[buttonId];
        IsActivated = true;
    }

    /**
     * <summary>Changes the state of the game when the player activates the button. Changes the color of the corresponding obstacle. Does this only when the button is activated.</summary>
     * <param name="other">The player which activates the button.</param>
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsActivated)
        {
            MainScript.CurrentState = CorrespondingNode.ChangeState(MainScript.CurrentState);
            CorrespondingEdge.ChangeColorOfObstacle(MainScript.CurrentState);
            IsActivated = false;
        }
    }
}
