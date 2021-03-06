﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot2 : MonoBehaviour
{
    private float speed = 12f;
    public Rigidbody2D body;
    public static Vector2 direction;
    private GameObject RubyObject;
    public GameObject DeathExplosion;

    private void Awake()
    {
        RubyObject = GameObject.Find("Ruby");
    }
    void Start()
    {
        body.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Ruby"))
        {
            Instantiate(DeathExplosion, RubyObject.transform.position, Quaternion.identity);
            NodeController.PlayerReset = true;
            RubyObject.transform.position = new Vector3(-8.75f, 4.75f, 0);
            Destroy(gameObject);
        }

    }
}
