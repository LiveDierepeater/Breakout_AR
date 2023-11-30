using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private new Rigidbody2D rigidbody2D;

    public Ball standardBall;
    
    public PlayerData.PlayerData playerData;
    //public float speed => playerData.DefaultSpeed;
    
    private float speed;
    private float currentSpeed;
    
    private float input;
    public bool anchored;

    public int startHitPoints = 10;
    private int currentHitPoints;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHitPoints = startHitPoints;
    }
    private void Start()
    {
        SetAllPlayerStatsDefault();
        SpawnNewBall();
    }

    private void Update()
    {
        void ReadInput()
        {
            input = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.E))
            {
                anchored = true;
                speed = 0f;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                anchored = false;
                speed = currentSpeed;
            }
        }

        ReadInput();
    }

    private void FixedUpdate()
    {
        // Movement
        rigidbody2D.velocity = new Vector2(input * speed, 0);
    }

    public void SpawnNewBall()
    {
        Ball newBall = Instantiate(standardBall, Vector3.up * 3, transform.rotation);
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
        
        gameManager.OverrideHitPoints(currentHitPoints);
    }
    
    public void ApplyPowerUp(Powerup powerUp)
    {
        print("received PowerUp");

        switch (powerUp.type)
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

    private void SetAllPlayerStatsDefault()
    {
        speed = playerData.DefaultSpeed;
        currentSpeed = speed;
    }
}
