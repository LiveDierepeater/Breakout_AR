using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GameManager")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        ScalePlayer();
        Destroy(this.gameObject);
    }

    private void ScalePlayer()
    {
        player.transform.localScale *= 1.1f;
    }
}
