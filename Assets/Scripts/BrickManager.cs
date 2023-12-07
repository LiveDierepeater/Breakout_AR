using System;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public Brick brickPrefab;
    public GameObject pointPrefab;

    public Vector2Int brickMatrix = new Vector2Int(9, 13);
    public Vector2 padding;

    private Brick[,] brickArray;
    private GameManager gameManager;

    private int currentWaveNumber;
    private int rows, columns;
    
    public Action<int> OnScoreChanged;
    private int internalCurrentScore;
    
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    private void Start()
    {
        SpawnNewBricks();
    }

    private void Brick_OnBrickHit(Brick brick)
    {
        CurrentScore += 1;
        Instantiate(pointPrefab, brick.transform.position, Quaternion.identity);
        
        if (AreAnyBricksActive() == false)
        {
            EndCurrentWave();
            gameManager.LoadNextScene();
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

        
        rows = brickMatrix.y;
        columns = brickMatrix.x;
        brickArray = new Brick[rows, columns];
        
        currentWaveNumber = GetCurrentWaveNumber();
        int omitIndex = 13 - currentWaveNumber;
        if (omitIndex < 0) omitIndex = 0;
        
        
        // Create standard brick formation.
        
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Brick newBrick = Instantiate(brickPrefab);

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

    private int GetCurrentWaveNumber()
    {
        return gameManager.GetCurrentWaveNumber();
    }
}
