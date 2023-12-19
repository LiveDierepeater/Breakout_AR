using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickManager : MonoBehaviour
{
    public Brick defaultBrickPrefab;
    public Brick brick_light;
    public Brick brick_medium;
    public Brick brick_hard;
    public GameObject pointPrefab;

    public Vector2Int brickMatrix = new Vector2Int(9, 13);
    public Vector2 padding;

    private Brick[,] brickArray;
    private GameManager gameManager;

    private float currentDifficulty;
    private int currentWaveNumber;
    private int rows, columns;
    
    public Action<int> OnScoreChanged;
    private int internalCurrentScore;

    private IEnumerator SpawningPoints;
    
    private int CurrentScore
    {
        get => internalCurrentScore;
        set
        {
            internalCurrentScore = value;
            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
    
    private void Awake()
    {
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
    }
    
    private void Start()
    {
        SpawnNewBricks();
    }

    private void Brick_OnBrickHit(Brick brick)
    {
        CurrentScore += brick.value;
        
        SpawningPoints = Spawning(brick, 0.07f);
        StartCoroutine(SpawningPoints);

        if (AreAnyBricksActive() == false)
        {
            EndCurrentWave();
            gameManager.LoadNextScene();
        }
    }

    private IEnumerator Spawning(Brick brick, float timeToWait)
    {
        Vector3 brickPosition = brick.transform.position;
        
        for (int x = 0; x < brick.value; x++)
        {
            Instantiate(pointPrefab, brickPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeToWait);
        }
    }
    
    private void EndCurrentWave()
    {
        ClearAllBricks();
        gameManager.DestroyAllCurrentBalls();
        
        // Show Upgrade UI
        gameManager.IsInUpgradePhase = true;
        
        // Deactivate Player Movement in "GameManager.IsInUpgradePhase.Set"
        // When Player presses O.K.
            // Hide Upgrade UI
                // Is Done in "UpgradeUI.SetIsInUpgradePhase()"     |   It sets the bool "IsInUpgradePhase" to false, when it gets called by "button.onClick()".
        
            // Activate Player Movement
                // Is Done in "GameManager.IsInUpgradePhase().Set"  |   It sets Player gameObject Active/Inactive when: "player.gameObject.SetActive(!value)".
    }

    public void GenerateNextWave()
    {
        // Generate new Bricks
        SpawnNewBricks();

        // Reset Player Position
        gameManager.ResetPlayerPosition(Vector3.zero);
        
        // Spawn new Ball
        gameManager.SpawnNewBall();
    }

    private void ClearAllBricks()
    {
        // Destroying all Bricks AND THEN clearing bricks-array
        
        for (int x = 0; x < brickMatrix.x; x++)
        {
            for (int y = 0; y < brickMatrix.y; y++)
            {
                Brick brick = brickArray[y, x];
                brickArray.SetValue(null, y, x);
                Destroy(brick.gameObject);
            }
        }
    }

    private void SpawnNewBricks()
    {
        // Look in which wave we are. Compare to previous amount of bricks in wave.
            // Create "BRICK MATRIX" to format bricks. Create an "INDEX OF OMIT" (out of "waveNumber") which will let out rows or columns.
            // Create "INDEX OF DIFFICULTY". This index will decide which Bricks will get spawned.

        rows = brickMatrix.y;
        columns = brickMatrix.x;
        brickArray = new Brick[rows, columns];
        
        currentWaveNumber = gameManager.GetCurrentWaveNumber();
        int omitIndex = 13 - currentWaveNumber;
        if (omitIndex < 0) omitIndex = 0;

        // Create standard brick formation.
        
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Brick newBrick = Instantiate(defaultBrickPrefab);
                newBrick.OnBrickHit += Brick_OnBrickHit;
                Vector3 brickScale = newBrick.transform.localScale;
                
                newBrick.transform.position = transform.position
                      + (Vector3.right * y * brickScale.x)
                      + (Vector3.right * y * padding.x)
                      + (Vector3.up * x * brickScale.y)
                      + (Vector3.up * x * padding.y);

                brickArray.SetValue(newBrick, x, y);
            }
        }
        
        // Disable Bricks dependent from "INDEX OF OMIT".

        int rowsToSpawn = brickMatrix.y - omitIndex;

        for (int y = rowsToSpawn; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Brick brick = brickArray[y, x];
                brick.gameObject.SetActive(false);
            }
        }
        
        MixStrongerBricksWithin();
    }

    private void MixStrongerBricksWithin()
    {
        // The "currentDifficulty" will decide here how many strong bricks will get spawned.
        
        currentDifficulty = currentWaveNumber - 2;
        
        if (currentDifficulty > 0)
        {
            // Consider "currentDifficulty" for the amount of stronger spawned bricks till a maximum difficulty (value) is reached.
            // Goes Through every active Bricks in "brickArray" and switches them when chance is given.

            if (currentWaveNumber >= 5)
              HigherDifficulty();
            else
            {
                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < columns; y++)
                    {
                        Brick brick = brickArray[x, y];
                        if (brick.isActiveAndEnabled)
                        {
                            float switchChance = currentDifficulty / 5f;
                            float roll = Random.Range(0f, 1f);

                            if (roll < switchChance) // Switches the current Brick out.
                            {
                                Brick newStrongerBrick = Instantiate(brick_light);
                                newStrongerBrick.OnBrickHit += Brick_OnBrickHit;
                                Vector3 brickScale = newStrongerBrick.transform.localScale;
                            
                                newStrongerBrick.transform.position = transform.position
                                                                      + (Vector3.right * y * brickScale.x)
                                                                      + (Vector3.right * y * padding.x)
                                                                      + (Vector3.up * x * brickScale.y)
                                                                      + (Vector3.up * x * padding.y);

                                brickArray.SetValue(newStrongerBrick, x, y);
                                Destroy(brick.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }

    private int stateCounter;
    
    private void HigherDifficulty()
    {
        if (currentWaveNumber < 10)
        {
            // Do first Iteration -> only light bricks and mixing medium bricks within.
            
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    Brick brick = brickArray[x, y];
                    if (brick.isActiveAndEnabled)
                    {
                        Brick lightBrick = Instantiate(brick_light);
                        lightBrick.OnBrickHit += Brick_OnBrickHit;
                        Vector3 lightBrickScale = lightBrick.transform.localScale;
                            
                        lightBrick.transform.position = transform.position
                                                              + (Vector3.right * y * lightBrickScale.x)
                                                              + (Vector3.right * y * padding.x)
                                                              + (Vector3.up * x * lightBrickScale.y)
                                                              + (Vector3.up * x * padding.y);

                        brickArray.SetValue(lightBrick, x, y);
                        Destroy(brick.gameObject);
                        
                        float switchChance = currentDifficulty / 10f;
                        float roll = Random.Range(0f, 1f);

                        if (roll < switchChance) // Switches the current Brick out for a stronger Brick.
                        {
                            Brick newStrongerBrick = Instantiate(brick_medium);
                            newStrongerBrick.OnBrickHit += Brick_OnBrickHit;
                            Vector3 brickScale = newStrongerBrick.transform.localScale;
                            
                            newStrongerBrick.transform.position = transform.position
                                                                  + (Vector3.right * y * brickScale.x)
                                                                  + (Vector3.right * y * padding.x)
                                                                  + (Vector3.up * x * brickScale.y)
                                                                  + (Vector3.up * x * padding.y);

                            brickArray.SetValue(newStrongerBrick, x, y);
                            Destroy(lightBrick.gameObject);
                        }
                    }
                }
            }
        }

        if (currentWaveNumber >= 10)
        {
            // Do second Iteration -> only medium bricks and mixing heavy bricks within.
            
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    Brick brick = brickArray[x, y];
                    if (brick.isActiveAndEnabled)
                    {
                        Brick mediumBrick = Instantiate(brick_medium);
                        mediumBrick.OnBrickHit += Brick_OnBrickHit;
                        Vector3 lightBrickScale = mediumBrick.transform.localScale;
                            
                        mediumBrick.transform.position = transform.position
                                                              + (Vector3.right * y * lightBrickScale.x)
                                                              + (Vector3.right * y * padding.x)
                                                              + (Vector3.up * x * lightBrickScale.y)
                                                              + (Vector3.up * x * padding.y);

                        brickArray.SetValue(mediumBrick, x, y);
                        Destroy(brick.gameObject);
                        
                        float switchChance = currentDifficulty / 15f;
                        float roll = Random.Range(0f, 1f);

                        if (roll < switchChance) // Switches the current Brick out for a stronger Brick.
                        {
                            Brick newStrongerBrick = Instantiate(brick_hard);
                            newStrongerBrick.OnBrickHit += Brick_OnBrickHit;
                            Vector3 brickScale = newStrongerBrick.transform.localScale;
                            
                            newStrongerBrick.transform.position = transform.position
                                                                  + (Vector3.right * y * brickScale.x)
                                                                  + (Vector3.right * y * padding.x)
                                                                  + (Vector3.up * x * brickScale.y)
                                                                  + (Vector3.up * x * padding.y);

                            brickArray.SetValue(newStrongerBrick, x, y);
                            Destroy(mediumBrick.gameObject);
                        }
                    }
                }
            }
        }
    }

    private bool AreAnyBricksActive()
    {
        // Goes through "brickArray" and returns false when each brick is inactive.
        
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (brickArray[y, x].gameObject.activeSelf) return true;
            }
        }
        return false;
    }
}
