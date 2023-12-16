using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private new Rigidbody2D rigidbody2D;

    public Ball standardBall;
    
    public PlayerData.PlayerData playerData;
    
    public int currentHitPoints;
    public int currentDamage;
    public int currentCriticalHitDamage;
    public float currentPlayerSpeed;
    public float currentCriticalHitChance;
    public float currentLuck;
    public float currentLoot;
    public Vector3 currentPlayerScale;

    private bool internalAnchored;
    private float input;

    public bool anchored
    {
        get => internalAnchored;
        set
        {
            internalAnchored = value;
            spriteRenderer.color = value ? Color.yellow : Color.white;
        }
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        SetAllPlayerStatsDefault();
        SpawnNewBall();
    }

    private void Update()
    {
        ReadInput();
        return;

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
