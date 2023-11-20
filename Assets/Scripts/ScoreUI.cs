using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private const string scoreLabel = "Score:";
    
    public BrickManager brickManager;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        
        brickManager.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        SetScoreText(newScore);
    }

    void SetScoreText(int score)
    {
        textComponent.text = scoreLabel + " " + score;
    }
}