using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    Brick brick;

    [Range(0, 1f)]
    public float spawnChance = 0.5f;
    
    public Powerup[] powerupPrefabs;

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
        bool willSpawnPowerup = Random.Range(0, 1f) <= spawnChance;
        if (willSpawnPowerup) SpawnPowerup();
    }

    void SpawnPowerup()
    {
        int randomIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}
