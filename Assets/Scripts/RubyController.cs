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
        //Variante 1 + 4 (ansonsten die beiden zeilen auskommentieren):
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;

        //Kann in jeder Variante so stehen gelassen werden:
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //In jeder Variante benötigt:
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Variante 1:
        //Vector2 position = transform.position;
        //position.x += 0.05f * horizontal;
        //position.y += 0.05f * vertical;
        //transform.position = position;

        //Variante 2:
        //Vector2 position = transform.position;
        //position.x += 3.5f * horizontal * Time.deltaTime;
        //position.y += 3.5f * vertical * Time.deltaTime;
        //transform.position = position;
    }

    void FixedUpdate()
    {
        //Variante 3 + 4:
        //Moves the player if the user triggers the arrow keys.
        //Vector2 position = rigidbody2d.position;
        //position.x += 0.065f * horizontal;
        //position.y += 0.065f * vertical;
        //rigidbody2d.MovePosition(position);

        //Variante 5:
        Vector2 position = rigidbody2d.position;
        position.x += 2.5f * horizontal * Time.deltaTime;
        position.y += 2.5f * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
