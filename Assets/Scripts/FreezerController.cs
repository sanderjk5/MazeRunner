using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerController : MonoBehaviour
{
    public NodeController CorrespondingNode { get; private set; }
    public void Initialize(NodeController correspondingNode)
    {
        CorrespondingNode = correspondingNode;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.Find("Opponent").GetComponent<BoxCollider2D>().Equals(collision))
        {
            StartCoroutine(GameObject.Find("Ruby").GetComponent<RubyController>().FreezePlayer(10));
        }
        else
        {
            StartCoroutine(GameObject.Find("Opponent").GetComponent<OpponentController>().FreezeOpponent(10));
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        MainScript.AllFreezer.Remove(this);
    }
}
