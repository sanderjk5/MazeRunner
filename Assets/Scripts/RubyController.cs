using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    float playerSpeed;

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
        position.x += playerSpeed * horizontal * Time.deltaTime;
        position.y += playerSpeed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void SetPositionAndScale()
    {
        if(MainScript.ScaleMazeSize == 0.5f)
        {
            gameObject.transform.position = new Vector3(-8.75f, 4.75f);
            gameObject.transform.localScale = new Vector3(0.175f, 0.175f);
            playerSpeed = 1.125f;
        } else
        {
            gameObject.transform.position = new Vector3(-8.5f, 4.5f);
            gameObject.transform.localScale = new Vector3(0.35f, 0.35f);
            playerSpeed = 2.25f;
        }
        
    }

    public IEnumerator FreezePlayer(int seconds)
    {
        //Rigidbody2D ruby = GameObject.Find("Ruby").GetComponent<Rigidbody2D>();
        //ruby.constraints = RigidbodyConstraints2D.FreezeAll;
        playerSpeed /= 2;
        yield return new WaitForSeconds(seconds);
        playerSpeed *= 2;
        //ruby.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
