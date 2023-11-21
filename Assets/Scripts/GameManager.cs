using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CanvasManager canvasManager;
    private Player player;
    private List<Ball> balls;
    private BrickManager brickManager;

    private int points;
    
    public event Action<bool> OnUpgradePhaseActive;
    private bool internalIsInUpgradePhase;
    
    public bool IsInUpgradePhase
    {
        // Is responsible for:
            // Calling out OnUpgradePhaseActive
            // Sets "player.gameObject" Active/Inactive
            // Calls "brickManager.GenerateNextWave()" to generate next wave, when "UpgradeUI.btn" (O.K.) has been pressed.
        
        get => internalIsInUpgradePhase;
        set
        {
            internalIsInUpgradePhase = value;
            OnUpgradePhaseActive?.Invoke(IsInUpgradePhase);
            player.gameObject.SetActive(!internalIsInUpgradePhase);  // Sets Player Inactive when value is false and sets active when value is true.
            if (!internalIsInUpgradePhase) brickManager.GenerateNextWave();
        }
    }

    private void Awake()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        balls = new List<Ball>();
        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();
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
        balls.Add(subscribedBall);
    }

    public void UnsubscribeBall(Ball unsubscribeBall)
    {
        balls.Remove(unsubscribeBall);
    }

    public void DestroyAllCurrentBalls()
    {
        // Destroys all current balls in "BrickManager.balls"
        foreach (Ball ball in balls) Destroy(ball.gameObject);
        balls.Clear();
    }

    public void SpawnNewBall()
    {
        player.SpawnNewBall();
    }
}
