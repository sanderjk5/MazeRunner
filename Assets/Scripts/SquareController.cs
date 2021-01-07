using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Quits the game if the player reaches the square.
     */
    void OnTriggerEnter2D(Collider2D other){
        Application.Quit();
    }
}
