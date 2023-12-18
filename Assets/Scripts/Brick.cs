using UnityEngine;

public class Brick : MonoBehaviour
{
    public delegate void BrickHitDelegate(Brick brickThatWasHit);
    public event BrickHitDelegate OnBrickHit;

    private SpriteRenderer spriteRenderer;

    public int startHP = 1;
    public Color color;
    public int value = 1;

    private int currentHP;

    private void OnEnable()
    {
        spriteRenderer = transform.Find("Brick_Highlight").GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Vector4(color.r, color.g, color.b, 1f);
        currentHP = startHP;
    }

    private void OnCollisionEnter2D()
    {
        Player player = GameObject.Find("Player").GetComponentInChildren<Player>();
        int damage = player.currentDamage;
        
        // TODO: Each time when the brick gets hit, the brick should roll whether he got hit critical or not and then applies the damage.
        
        currentHP -= damage;
        if (currentHP <= 0) DeactivateBrick();
    }

    private void DeactivateBrick()
    {
        gameObject.SetActive(false);
        OnBrickHit?.Invoke(this);
    }
}
