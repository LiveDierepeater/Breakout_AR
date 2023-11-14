using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI currentWaveUI;
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI hpUI;

    private string waveName = "Wave: ";
    private int currentWaveNumber = 1;

    private string pointName = "Points: ";

    private string hpName = " HP";

    private void Start()
    {
        currentWaveUI.text = waveName + currentWaveNumber;
        pointsUI.text = pointName + "0";
        hpUI.text = "3" + hpName;
    }

    public void OverridePoints(int points)
    {
        pointsUI.text = pointName + points;
    }

    public void NextWaveNumbre()
    {
        currentWaveUI.text = waveName + (currentWaveNumber += 1);
    }

    public void OverrideHitPoints(int hP)
    {
        hpUI.text = hP + hpName;
    }

    public int GetCurrentWaveNumber()
    {
        return currentWaveNumber;
    }
}
