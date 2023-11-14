using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rigidbody2D;

    public Ball standartBall;

    public float speed = 15f;
    public bool anchored;
    
    private float input;
    private float normalSpeed;

    public int startHitPoints = 3;
    private int currentHitPoints;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        normalSpeed = speed;
        currentHitPoints = startHitPoints;
    }
    private void Start()
    {
        Ball newBall = Instantiate(standartBall, Vector3.up, transform.rotation);
        newBall.OnBallOut += Ball_OnBallOut;
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

    private void SpawnNewBall()
    {
        Ball newBall = Instantiate(standartBall, Vector3.up * 3, transform.rotation);
        newBall.OnBallOut += Ball_OnBallOut;
        newBall.gameObject.name = "Ball_" + newBall.GetInstanceID().ToString();
    }

    private void Ball_OnBallOut()
    {
        CheckHitPoints();
    }

    private void CheckHitPoints()
    {
        if (currentHitPoints > 1)
        {
            currentHitPoints--;
            SpawnNewBall();
        }
        else if (currentHitPoints == 1)
        {
            currentHitPoints--;
            print("DEAD!: " + currentHitPoints);
        }
        
        gameManager.OverrideHP(currentHitPoints);
    }
    
    public void ApplyPowerup(Powerup powerup)
    {
        print("received powerup");

        switch (powerup.type)
        {
            case Powerup.PowerupType.None:
                break;
            
            case Powerup.PowerupType.Stretch:
                transform.localScale += Vector3.right * 0.25f;
                break;
            
            case Powerup.PowerupType.Shrink:
                transform.localScale -= Vector3.right * 0.25f;
                break;
        }
    }
}
