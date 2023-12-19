using UnityEngine;

public class Brick : MonoBehaviour
{
    public delegate void BrickHitDelegate(Brick brickThatWasHit);
    public event BrickHitDelegate OnBrickHit;

    private SpriteRenderer spriteRenderer;
    private SoundManager soundManager;

    public int startHP = 1;
    public Color color;
    public int value = 1;

    public CriticalHitSpawner criticalHit;
    
    private int currentHP;

    private void OnEnable()
    {
        spriteRenderer = transform.Find("Brick_Highlight").GetComponentInChildren<SpriteRenderer>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        spriteRenderer.color = new Vector4(color.r, color.g, color.b, 1f);
        currentHP = startHP;
    }

    private void OnCollisionEnter2D()
    {
        // Each time when the brick gets hit, the brick rolls whether he got hit critical or not and then applies the damage.
        Player player = GameObject.Find("Player").GetComponentInChildren<Player>();
        int damage;
        if (IsCriticalHit(player))
        {
            damage = player.currentCriticalHitDamage;
            Instantiate(criticalHit, transform.position, Quaternion.identity, null);
            soundManager.PlayHitSound_Critical();
        }
        else
            damage = player.currentDamage;
        
        currentHP -= damage;
        if (currentHP <= 0) DeactivateBrick();
    }

    private void DeactivateBrick()
    {
        gameObject.SetActive(false);
        OnBrickHit?.Invoke(this);
    }

    private bool IsCriticalHit(Player player)
    {
        // Each time when the brick gets hit, it rolls whether he got hit critical or not.
        float roll = Random.Range(0, 1f);
        bool isHit = roll < player.currentCriticalHitChance;
        return isHit;
    }
}
