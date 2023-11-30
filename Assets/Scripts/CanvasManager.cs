using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI currentWaveUI;
    public TextMeshProUGUI hpUI;
    public PlayerData.PlayerData playerData;

    private const string waveName = "Wave: ";
    private int currentWaveNumber = 1;

    private const string hpName = " HP";

    private void Start()
    {
        currentWaveUI.text = waveName + currentWaveNumber;
        hpUI.text = playerData.DefaultMaxHitPoints + hpName;
    }

    public void NextWaveNumber()
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
