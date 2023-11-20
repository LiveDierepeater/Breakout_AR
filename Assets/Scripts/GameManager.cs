using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CanvasManager canvasManager;

    private int points;

    private void Awake()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
    }

    private void Update()
    {
        // Level Reset Button
        if (Input.GetKey(KeyCode.R))
            ReloadLevel();
    }

    public void OverrideHitPoints(int currentHitPoints)
    {
        canvasManager.OverrideHitPoints(currentHitPoints);
    }

    // TODO: move this somewhere else?
    public void LoadNextScene()
    {
        canvasManager.NextWaveNumber();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetCurrentWaveNumber()
    {
        return canvasManager.GetCurrentWaveNumber();
    }
}
