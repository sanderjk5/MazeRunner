using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private float speed = 12f;
    public Rigidbody2D body;
    public static Vector2 direction;
    private GameObject OpponentObject;
    public GameObject DeathExplosion;

    private void Awake()
    {
        OpponentObject = GameObject.Find("Opponent");
    }
    void Start()
    {
        body.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name.Equals("Opponent"))
        {
            Instantiate(DeathExplosion, OpponentObject.transform.position, Quaternion.identity);
            GameObject.Find("Opponent").GetComponent<OpponentController>().ResetPosition();
        }
    }
}  
