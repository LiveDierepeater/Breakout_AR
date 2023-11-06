using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CanvasManager canvasManager;
    private BrickManager brickManager;

    private int points;

    private void Awake()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();
    }

    private void Update()
    {
        // Level Reset Button
        if (Input.GetKey(KeyCode.R))
            ReloadLevel();
    }

    public void CalculatePoints(int value)
    {
        //TODO: Later different Brick "Value" will have different outcome to "Points"
        points += value;
        
        canvasManager.OverridePoints(points);
    }

    // TODO: move this somewhere else?
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int targetSceneIndex = currentSceneIndex + 1;

        // [insert reason why we wrap back to scene 0]
        if (targetSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            targetSceneIndex = 0;
        }

        SceneManager.LoadScene(targetSceneIndex);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
