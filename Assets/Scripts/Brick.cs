using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.activeBricks++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.activeBricks--;
        DestroyObject(gameObject);
    }
}