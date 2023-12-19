using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        None,
        Stretch,
        Shrink,
        LightingStrike,
    }

    public PowerupType type;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        Player player = collision.gameObject.GetComponent<Player>();
        player.ApplyPowerUp(this);
    
        Destroy(gameObject);
    }
    
}
