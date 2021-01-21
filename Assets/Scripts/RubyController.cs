using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        //Moves the player if the user triggers the arrow keys.
        Vector2 position = rigidbody2d.position;
        position.x += 0.065f * horizontal;
        position.y += 0.065f * vertical;
        rigidbody2d.MovePosition(position);
    }
}
