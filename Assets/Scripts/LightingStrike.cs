using UnityEngine;

public class LightingStrike : MonoBehaviour
{
    private Player player;

    public int strikeDamage;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        strikeDamage = player.currentCriticalHitDamage;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Do nothing when hitting not a brick.
        if (!other.CompareTag("Brick")) return;

        // Do stuff when brick was hit
        //other.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 0f, 0f);
    }

    public int GetPlayersCurrentCriticalHitDamage()
    {
        return player.currentCriticalHitDamage;
    }
}