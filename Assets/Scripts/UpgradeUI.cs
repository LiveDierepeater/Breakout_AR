using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnUpdradePhaseActive += ActivateUpgradeUI;
    }

    private void ActivateUpgradeUI(bool isActive)
    {
        
    }
}
