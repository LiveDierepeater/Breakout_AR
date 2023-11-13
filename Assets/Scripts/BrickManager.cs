using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public Brick brickPrefab;

    public Vector2Int amount;
    public Vector2 padding;

    private List<Brick> bricks;
    private GameManager gameManager;

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

                newBrick.transform.position = transform.position
                    + (Vector3.right * x * newBrick.transform.localScale.x)
                    + (Vector3.right * x * padding.x)
                    + (Vector3.up * y * newBrick.transform.localScale.y)
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
        // TODO: Delete all unactive Bricks
        ClearAllBricks();

        // TODO: Generate Brick Formation (anzahl x und y | bestimmte reihen nicht benutzen | steigend immer mehr bricks bis zu einem gewissen punkt)


        // TODO: Generate new Bricks
        SpawnBricks();
    }

    private void ClearAllBricks()
    {
        foreach (Brick brick in bricks)
        {
            if (brick != null)
                Destroy(brick.gameObject);
        }

        bricks.Clear();
    }

    private bool AreAnyBricksActive()
    {
        foreach (Brick brick in bricks)
        {
            if (brick.gameObject.activeSelf) return true;
        }

        return false;
    }
}
