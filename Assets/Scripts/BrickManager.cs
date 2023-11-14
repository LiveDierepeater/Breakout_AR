using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public Brick brickPrefab;

    public Vector2Int amount;
    public Vector2Int brickMatrix = new Vector2Int(9, 13);
    public Vector2 padding;

    Brick[,] brickArray;
    private GameManager gameManager;

    private int currentWaveNumber;

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

        if (AreAnyBricksActive() == false)
        {
            GenerateNextWave();
            gameManager.LoadNextScene();
        }
    }

    private void GenerateNextWave()
    {
        // Delete all unactive Bricks
        ClearAllBricks();
        
        // Generate new Bricks
        SpawnNewBricks();
    }

    // Destroying all Bricks AND THEN clearing <list> "bricks"
    private void ClearAllBricks()
    {
        for (int x = 0; x < brickMatrix.x; x++)
        {
            for (int y = 0; y < brickMatrix.y; y++)
            {
                Brick brick = brickArray[x, y];
                brickArray.SetValue(null, x, y);
                Destroy(brick.gameObject);
            }
        }
    }

    private void SpawnNewBricks()
    {
        // Look in which wave we are. Compare to previous amount of bricks in wave.
            // Create "BRICK MATRIX" to format bricks. Create an "INDEX OF OMIT" (out of "waveNumber") which will let out rows or columns.

        int column, rows;
        column = brickMatrix.x;
        rows = brickMatrix.y;
        brickArray = new Brick[column, rows];
        
        currentWaveNumber = GetCurrentWaveNumber();
        int omitIndex = 13 - currentWaveNumber;
        if (omitIndex < 0) omitIndex = 0;
        
        
        // Create formation in consideration of "INDEX OF OMIT".
        
        for (int x = 0; x < column; x++)
        {
            for (int y = 0; y < rows; y++)
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

        for (int x = rowsToSpawn; x < column; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Brick brick = brickArray[x, y];
                brick.gameObject.SetActive(false);
            }
        }
    }
    
    private bool AreAnyBricksActive()
    {
        for (int x = 0; x < brickMatrix.x; x++)
        {
            for (int y = 0; y < brickMatrix.y; y++)
            {
                if (brickArray[x, y].gameObject.activeSelf) return true;
            }
        }
        return false;
    }

    private int GetCurrentWaveNumber()
    {
        return gameManager.GetCurrentWaveNumber();
    }
}
