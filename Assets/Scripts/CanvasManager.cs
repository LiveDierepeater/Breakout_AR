using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI currentWaveUI;
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI hpUI;

    private const string WaveName = "Wave: ";
    private int currentWaveNumber = 1;

    private const string PointName = "Points: ";

    private const string HpName = " HP";

    private void Start()
    {
        currentWaveUI.text = WaveName + currentWaveNumber;
        pointsUI.text = PointName + "0";
        hpUI.text = "3" + HpName;
    }

    public void OverridePoints(int points)
    {
        pointsUI.text = PointName + points;
    }

    public void NextWaveNumber()
    {
        currentWaveUI.text = WaveName + (currentWaveNumber += 1);
    }

    public void OverrideHitPoints(int hP)
    {
        hpUI.text = hP + HpName;
    }

    public int GetCurrentWaveNumber()
    {
        return currentWaveNumber;
    }
}
