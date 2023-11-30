using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private GameManager gameManager;
    private Button exitButton;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnUpgradePhaseActive += SWITCH_UpgradeUI;
        gameObject.SetActive(false);

        AddOnClickListenerToExitButton();
    }
    
    private void SWITCH_UpgradeUI(bool isActive)
    {
        switch (isActive)
        {
            case true:
                gameObject.SetActive(true);
                break;
            
            case false:
                // TODO: Fill with actions that will happen when OnUpgradePhaseActive gets false / Player pressed OK_btn.
                gameObject.SetActive(false);
                break;
        }
    }

    private void SetIsInUpgradePhase()
    {
        gameManager.IsInUpgradePhase = false;
    }
    
    private void AddOnClickListenerToExitButton()
    {
        Transform okButton = transform.Find("OK_btn");
        exitButton = okButton.GetComponentInChildren<Button>();
        exitButton.onClick.AddListener(SetIsInUpgradePhase);
    }
}
