using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public EdgeController CorrespondingEdge { get; private set; }
    public NodeController CorrespondingNode { get; private set; }

    public void Initialize(EdgeController correspondingEdge, NodeController correspondingNode, int buttonId)
    {
        CorrespondingEdge = correspondingEdge;
        CorrespondingNode = correspondingNode;
        gameObject.GetComponent<SpriteRenderer>().color = MainScript.Colors[buttonId];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        MainScript.CurrentState = CorrespondingNode.ChangeState(MainScript.CurrentState);
        CorrespondingEdge.ChangeColorOfObstacle(MainScript.CurrentState);
    }
}
