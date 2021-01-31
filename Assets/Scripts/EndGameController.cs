using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        // Freeze the character
        Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        ruby.constraints = RigidbodyConstraints2D.FreezeAll;

        // Set the end game variable to true
        EndGameMenu.EndGotReached = true;
    }
}
