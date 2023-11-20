using System;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public Brick brickPrefab;

    public Vector2Int amount;
    public Vector2Int brickMatrix = new Vector2Int(9, 13);
    public Vector2 padding;

    private Brick[,] brickArray;
    private GameManager gameManager;

    private int currentWaveNumber;
    private int rows, columns;
    
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
    
    public Action<int> OnScoreChanged;
    
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
        gameManager.CalculatePoints(brick.value);

        CurrentScore += 1;
        
        if (AreAnyBricksActive() == false)
        {
            GenerateNextWave();
            gameManager.LoadNextScene();
        }
    }

    private void GenerateNextWave()
    {
        // Delete all inactive Bricks
        ClearAllBricks();
        
        // Generate new Bricks
        SpawnNewBricks();
    }

    // Destroying all Bricks AND THEN clearing bricks-array
    private void ClearAllBricks()
    {
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
