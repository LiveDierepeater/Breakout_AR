using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public delegate void BallOutDelegate();
    public event BallOutDelegate OnBallOut;

    private Rigidbody2D rigidbody2D;
    private Player player;

    public Vector2 initialVelocity = Vector2.up * 5;
    public float deflection = 1f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnBallOut?.Invoke();
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
