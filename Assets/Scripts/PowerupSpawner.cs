using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    private Brick brick;
    private Player player;

    [Range(0, 1f)]
    public float standardSpawnChance = 0.05f;

    private float spawnChance;
    
    public Powerup[] powerUpPrefabs;

    private void Awake()
    {
        brick = GetComponent<Brick>();
        if (brick != null)
        {
            brick.OnBrickHit += Brick_OnBrickHit;
        }

        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        spawnChance = standardSpawnChance + player.currentLuck;
    }

    private void Brick_OnBrickHit(Brick brickThatWasHit)
    {
        bool willSpawnPowerUp = Random.Range(0, 1f) <= spawnChance;
        if (willSpawnPowerUp) SpawnPowerUp();
    }

    private void SpawnPowerUp()
    {
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}
