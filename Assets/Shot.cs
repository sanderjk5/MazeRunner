using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private float speed = 12f;
    public Rigidbody2D body;
    public static Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        body.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}  
