using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private const string scoreLabel = "Score:";
    
    private BrickManager brickManager;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();
        
        brickManager.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        SetScoreText(newScore);
    }

    private void SetScoreText(int score)
    {
        textComponent.text = scoreLabel + " " + score;
    }
}