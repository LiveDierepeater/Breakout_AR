using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private const string scoreLabel = "Score:";
    private int currentPoints;
    
    private BrickManager brickManager;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();
        
        brickManager.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        currentPoints = newScore;
        textComponent.text = scoreLabel + " " + currentPoints;
    }

    public void PayWithPoints(int costs)
    {
        currentPoints -= costs;
        textComponent.text = scoreLabel + " " + currentPoints;
    }
}
