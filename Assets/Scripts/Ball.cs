using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    GameManager gameManager;
    Player player;

    public Vector2 initialVelocity = Vector2.up * 5;
    public float deflection = 1f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        print(player);
    }

    private void Start()
    {
        rigidbody2D.velocity = initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {
                CheckIfPlayerAnchored();
                
                if (!player.anchored)
                {
                    float ballPositionX = transform.position.x;
                    float playerPositionX = collision.gameObject.transform.position.x;

                    //calculates bouncePosition from ball on X Axis & multiplies it with the current player velocity.x
                    float bouncePositionX = ballPositionX - playerPositionX;
                    float bounceVelocityX = bouncePositionX * (collision.relativeVelocity.x + 1);

                    float currentSpeed = rigidbody2D.velocity.magnitude;
                    Vector2 newDirection = new Vector2(rigidbody2D.velocity.x + bounceVelocityX * deflection, rigidbody2D.velocity.y);

                    rigidbody2D.velocity = newDirection.normalized * currentSpeed;
                }
            }
        
        if (collision.gameObject.CompareTag("Brick"))
        {
            gameManager.AddPoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void CheckIfPlayerAnchored()
    {
        if (player.anchored)
        {
            CalculateAnchoredBounce();
        }
    }
    
    private void CalculateAnchoredBounce()
    {
        rigidbody2D.velocity = new Vector2(-rigidbody2D.velocity.x, rigidbody2D.velocity.y);
    }
}