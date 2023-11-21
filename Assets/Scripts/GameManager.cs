using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CanvasManager canvasManager;
    private Player player;
    private Ball ball;

    private int points;
    
    public event Action<bool> OnUpgradePhaseActive;
    private bool internalIsInUpgradePhase;
    
    public bool IsInUpgradePhase
    {
        get => internalIsInUpgradePhase;
        set
        {
            internalIsInUpgradePhase = value;
            OnUpgradePhaseActive?.Invoke(IsInUpgradePhase);
            player.gameObject.SetActive(!internalIsInUpgradePhase);  // Sets Player Inactive when value is false and sets active when value is true.
        }
    }

    private void Awake()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
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

    // TODO: May remove in future?
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
    
    public void SubscribeBall(Ball subscribedBall)
    {
        ball = subscribedBall;
    }

    public void DestroyCurrentBall()
    {
        Destroy(ball);
    }

    public void SpawnNewBall()
    {
        player.SpawnNewBall();
    }
}
