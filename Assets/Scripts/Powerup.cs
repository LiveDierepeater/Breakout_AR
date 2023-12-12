using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        None,
        Stretch,
        Shrink,
    }

    public PowerupType type;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        Player player = collision.gameObject.GetComponent<Player>();
        player.ApplyPowerUp(this);

        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Effect_ScalePlayer(player);
        Destroy(this.gameObject);
    }

    private void Effect_ScalePlayer(Player player)
    {
        if (player.transform.localScale.x <= 2f)
            player.transform.localScale *= 1.1f;
    }
    
}
