using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public Brick brickPrefab;

    public Vector2Int amount;
    public Vector2 padding;

    private List<Brick> bricks;
    private GameManager gameManager;

    private int currentWaveNumbre;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        SpawnBricks();
    }

    void SpawnBricks()
    {
        bricks = new List<Brick>();

        for (int y = 0; y < amount.y; y++)
        {
            for (int x = 0; x < amount.x; x++)
            {
                Brick newBrick = Instantiate(brickPrefab);

                newBrick.OnBrickHit += Brick_OnBrickHit;

                Vector3 brickScale = newBrick.transform.localScale;
                
                newBrick.transform.position = transform.position
                    + (Vector3.right * x * brickScale.x)
                    + (Vector3.right * x * padding.x)
                    + (Vector3.up * y * brickScale.y)
                    + (Vector3.up * y * padding.y);

                bricks.Add(newBrick);
            }
        }
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
        
        // TODO: Generate Brick Formation (anzahl x und y | bestimmte reihen nicht benutzen | steigend immer mehr bricks von wave zu wave bis zu einem gewissen punkt)
        GenerateNewBrickFormation();
        
        // TODO: Generate new Bricks
        SpawnBricks();
    }

    // Destroying all Bricks AND THEN clearing <list> "bricks"
    private void ClearAllBricks()
    {
        foreach (Brick brick in bricks)
        {
            if (brick != null)
                Destroy(brick.gameObject);
        }

        bricks.Clear();
    }

    private void GenerateNewBrickFormation()
    {
        // TODO: Look in which wave we are. Compare to previous amount of bricks in wave.
            // TODO: Create "BRICK MATRIX" to format bricks. Create an "INDEX OF OMIT" which will let out rows or columns.
        
        
        
        
        // TODO: Decide how many rows and columns will get generated.
        
        // TODO: Create formation in consideration of "INDEX OF OMIT".
        
        // TODO: Give important information to next function() to spawn new bricks.
    }
    
    
    
    
    
    
    
    
    
    
    private bool AreAnyBricksActive()
    {
        foreach (Brick brick in bricks)
        {
            if (brick.gameObject.activeSelf) return true;
        }

        return false;
    }

    private void GetCurrentWaveNumbre()
    {
        currentWaveNumbre = gameManager.GetCurrentWaveNumbre();
    }
}
