using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnUpdradePhaseActive += ActivateUpgradeUI;
        gameObject.SetActive(false);
    }

    private void ActivateUpgradeUI(bool isActive)
    {
        switch (isActive)
        {
            case true:
                gameObject.SetActive(true);
                break;
            
            case false:
                // TODO: Fill with actions that will happen when OnUpgradePhaseActive gets false.
                gameObject.SetActive(false);
                break;
        }
    }
}
