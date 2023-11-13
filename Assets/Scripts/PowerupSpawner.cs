using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    Brick brick;

    public GameObject powerupPrefab;

    private void Awake()
    {
        brick = GetComponent<Brick>();
        if (brick != null)
        {
            brick.OnBrickHit += Brick_OnBrickHit;
        }
    }

    private void Brick_OnBrickHit(Brick brickThatWasHit)
    {
        int diceRoll = Random.Range(0, 6);
        if (diceRoll == 0)
        {
        }
        SpawnPowerup();
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, transform.position, Quaternion.identity);
    }
}
