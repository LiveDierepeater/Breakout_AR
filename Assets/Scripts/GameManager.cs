using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject standartBall;

    private int points;
    private int startHP = 3;
    private int currentHP;

    public int activeBricks;

    public Vector3 spawnPosition;

    private void Awake()
    {
        currentHP = startHP;
        spawnPosition = new Vector3(0, 1, 0);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
            ReloadLevel();

        if (activeBricks == 0)
        {
            print("All bricks destroyed");
            LoadNextScene();
        }
    }

    public void AddPoint()
    {
        points++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentHP > 1)
        {
            currentHP--;
            print("Current HP: " + currentHP);
            SpawnNewBall();
        }
        else if (currentHP == 1)
        {
            currentHP--;
            print("DEAD!: " + currentHP);
            ReloadLevel();
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SpawnNewBall()
    {
        GameObject newball = Instantiate(standartBall);
        newball.transform.position = spawnPosition;
    }

    void LoadNextScene()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int targetSceneBuildIndex = currentSceneBuildIndex + 1;

        //check if target index is out of bounds.   [When there is no more next scene, set targetSceneBuildIndex to first scene]
        if (targetSceneBuildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            targetSceneBuildIndex = 0;
        }

        SceneManager.LoadScene(targetSceneBuildIndex);
    }
}