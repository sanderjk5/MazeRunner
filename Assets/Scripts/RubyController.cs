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
        if (MainScript.EnableUserInput)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if(MainScript.ScaleMazeSize == 0.5f)
        {
            position.x += 1.125f * horizontal * Time.deltaTime;
            position.y += 1.125f * vertical * Time.deltaTime;
        } else
        {
            position.x += 2.25f * horizontal * Time.deltaTime;
            position.y += 2.25f * vertical * Time.deltaTime;
        }
        
        rigidbody2d.MovePosition(position);
    }

    public void SetPositionAndScale()
    {
        if(MainScript.ScaleMazeSize == 0.5f)
        {
            gameObject.transform.position = new Vector3(-8.75f, 4.75f);
            gameObject.transform.localScale = new Vector3(0.175f, 0.175f);
        } else
        {
            gameObject.transform.position = new Vector3(-8.5f, 4.5f);
            gameObject.transform.localScale = new Vector3(0.35f, 0.35f);
        }
        
    }
}
