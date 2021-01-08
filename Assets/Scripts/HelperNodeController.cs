using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperNodeController : MonoBehaviour
{
    public int Id { get; private set; }
    public int State { get; private set; }
    public int Distance { get; private set; }
    public HelperNodeController Predecessor { get; private set; }
    
    public void Initialize(int id, int state, int distance, HelperNodeController predecessor)
    {
        Id = id;
        State = state;
        Distance = distance;
        Predecessor = predecessor;
    }
}
