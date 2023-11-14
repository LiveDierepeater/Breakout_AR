using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GameManager")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Effect_ScalePlayer(player);
        Destroy(this.gameObject);
    }

    private void Effect_ScalePlayer(Player player)
    {
        player.transform.localScale *= 1.1f;
    }
}
