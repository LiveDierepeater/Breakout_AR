using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour
{
    private static int activeBricks = 0;

    private void Awake()
    {
        activeBricks++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        activeBricks--;

        if (activeBricks == 0)
        {
            print("All bricks destroyed");
            LoadNextScene();
        }
        DestroyObject(gameObject);
    }

    // TODO: move this somewhere else?
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