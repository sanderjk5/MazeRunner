using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperNodeController : MonoBehaviour
{
    //The Id of the HelperNode.
    public int Id { get; private set; }
    //The current state of the HelperNode.
    public int State { get; private set; }
    //The current distance of the HelperNode.
    public int Distance { get; private set; }
    //The predecessor of the HelperNode.
    public HelperNodeController Predecessor { get; private set; }
    
    /**
     * Sets all variables of the HelperNode.
    */
    public void Initialize(int id, int state, int distance, HelperNodeController predecessor)
    {
        Id = id;
        State = state;
        Distance = distance;
        Predecessor = predecessor;
    }
}
