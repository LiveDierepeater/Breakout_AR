using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PointsUI : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private BrickManager brickManager;
    private GameManager gameManager;
    
    private int currentPoints;
    
    private const string pointsLabel = "Points:";
    
    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnUpgradePhaseActive += SWITCH_PointsUI;
        
        brickManager.OnScoreChanged += OnScoreChanged;
    }
    
    private void SWITCH_PointsUI(bool isActive)
    {
        switch (isActive)
        {
            case true:
                gameObject.SetActive(true);
                break;
            
            case false:
                gameObject.SetActive(false);
                break;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnScoreChanged(int newPoints)
    {
        currentPoints++;
        textComponent.text = pointsLabel + " " + currentPoints;
    }

    private void PayWithPoints(int costs)
    {
        currentPoints -= costs;
        textComponent.text = pointsLabel + " " + currentPoints;
    }

    public bool DoPlayerHaveEnoughPoints(int costs)
    {
        if (currentPoints - costs < 0) return false;
        PayWithPoints(costs);
        return true;
    }
}
