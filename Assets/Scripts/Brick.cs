using UnityEngine;

public class Brick : MonoBehaviour
{
    public delegate void BrickHitDelegate(Brick brickThatWasHit);
    public event BrickHitDelegate OnBrickHit;

    public int value = 1;

    private void OnCollisionEnter2D()
    {
        gameObject.SetActive(false);

        OnBrickHit?.Invoke(this);
    }
}