using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private BrickManager brickManager;
    
    private int currentPoints;
    
    private const string scoreLabel = "Score:";

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

    public int GetCurrentScore()
    {
        return currentPoints;
    }
}
