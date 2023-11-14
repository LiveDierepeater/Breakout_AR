using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rigidbody2D;

    public Ball standartBall;

    public float speed = 15f;
    public bool anchored = false;
    
    private float input;
    private float normalSpeed;

    public int startHP = 3;
    private int currentHP;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        normalSpeed = speed;
        currentHP = startHP;
    }
    private void Start()
    {
        Ball newBall = Instantiate(standartBall, Vector3.up, transform.rotation);
        newBall.OnBallOut += Ball_OnBallOut;
    }

    private void Update()
    {
        input = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.E))
        {
            anchored = true;
            speed = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anchored = false;
            speed = normalSpeed;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(input * speed, 0);
    }

    private void SpawnNewBall()
    {
        Ball newBall = Instantiate(standartBall, Vector3.up * 3, transform.rotation);
        newBall.OnBallOut += Ball_OnBallOut;
        newBall.gameObject.name = "Ball_" + newBall.GetInstanceID().ToString();
    }

    private void Ball_OnBallOut()
    {
        CheckHP();
    }

    private void CheckHP()
    {
        if (currentHP > 1)
        {
            currentHP--;
            SpawnNewBall();
        }
        else if (currentHP == 1)
        {
            currentHP--;
            print("DEAD!: " + currentHP);
        }
        
        gameManager.OverrideHP(currentHP);
    }
}
