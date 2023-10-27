using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    public float speed = 15f;
    public bool anchored = false;
    
    private float input;
    private float normalSpeed;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        normalSpeed = speed;
    }

    private void Update()
    {
        input = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.E))
        {
            anchored = true;
            speed = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anchored = false;
            speed = normalSpeed;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(input * speed, 0);
    }
}