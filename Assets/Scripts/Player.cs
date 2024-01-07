using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private new Rigidbody2D rigidbody2D;
    private SoundManager soundManager;

    public Ball standardBall;
    public LightingStrike lightingStrike;
    
    public PlayerData.PlayerData playerData;
    
    public int currentHitPoints;
    public int currentDamage;
    public int currentCriticalHitDamage;
    public float currentPlayerSpeed;
    public float currentCriticalHitChance;
    public float currentLuck;
    public int currentLoot;
    public Vector3 currentPlayerScale;

    public Ball currentBall;

    public bool isAttacking;
    
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
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        SetAllPlayerStatsDefault();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (lightingStrike.gameObject.activeSelf) return; // If Lighting Strike is currently active, return.

            if (currentBall != null)
            {
                Destroy(currentBall.gameObject);
                gameManager.UnsubscribeBall(currentBall);
            }
            
            // Plays Sound of Lighting Strike
            soundManager.PlayLightingStrikeSound();
            
            StartCoroutine(nameof(EnableLightingStrike));
        }
        
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
        currentBall = Instantiate(standardBall, Vector3.up * 3, transform.rotation);
        currentBall.OnBallOut += Ball_OnBallOut;
        currentBall.gameObject.name = "Ball";
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
        switch (powerUp.type)
        {
            case Powerup.PowerupType.None:
                break;
            
            case Powerup.PowerupType.Stretch:
                if (transform.localScale.x < 2f)
                    transform.localScale += Vector3.right * 0.25f;
                break;
            
            case Powerup.PowerupType.Shrink:
                if (transform.localScale.x > 0.5f)
                    transform.localScale -= Vector3.right * 0.25f;
                break;
            
            case Powerup.PowerupType.LightingStrike:
                if (!isAttacking)
                {
                    DoLightingStrike();
                }
                break;
        }
    }

    private void DoLightingStrike()
    {
        // Lighting Strike is currently active
        isAttacking = true;

        if (currentBall != null)
        {
            Destroy(currentBall.gameObject);
            gameManager.UnsubscribeBall(currentBall);
        }

        // Plays Sound of Lighting Strike
        soundManager.PlayLightingStrikeSound();
        
        // Starts Lighting Strike
        StartCoroutine(nameof(EnableLightingStrike));
    }

    public IEnumerator EnableLightingStrike()
    {
        // Active lighting and do stuff
        yield return new WaitForSeconds(0.5f);
        lightingStrike.gameObject.SetActive(true);
        
        // Deactivate after time
        yield return new WaitForSeconds(1f);
        lightingStrike.gameObject.SetActive(false); // Deactivates Lighting Strike Attack
        SpawnNewBall();
        isAttacking = false;
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
