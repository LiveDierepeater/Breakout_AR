using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private new Rigidbody2D rigidbody2D;

    public Ball standardBall;
    
    public PlayerData.PlayerData playerData;
    
    private int currentHitPoints;
    private int currentDamage;
    private int currentCriticalHitDamage;
    private float currentPlayerSpeed;
    private float currentCriticalHitChance;
    private float currentLuck;
    private float currentLoot;
    private Vector3 currentPlayerScale;
    
    private float input;
    public bool anchored;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
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
                rigidbody2D.velocity = Vector2.zero;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                anchored = false;
            }
        }

        ReadInput();
    }

    private void FixedUpdate()
    {
        // Movement
        if (!anchored)
            rigidbody2D.velocity = new Vector2(input * currentPlayerSpeed, 0);
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
        currentHitPoints = playerData.DefaultMaxHitPoints;
        currentDamage = playerData.DefaultDamage;
        currentCriticalHitDamage = playerData.DefaultCriticalHitDamage;
        currentPlayerSpeed = playerData.DefaultSpeed;
        currentCriticalHitChance = playerData.DefaultCriticalHitChance;
        currentLuck = playerData.DefaultLuck;
        currentLoot = playerData.DefaultLoot;
        currentPlayerScale = playerData.DefaultPlayerScale;
    }
}
