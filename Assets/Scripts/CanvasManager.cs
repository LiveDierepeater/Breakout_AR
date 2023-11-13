using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI currentWaveUI;
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI hpUI;

    private string waveName = "Wave: ";
    private int waveNumbre = 1;

    private string pointName = "Points: ";

    private string hpName = " HP";

    private void Start()
    {
        currentWaveUI.text = waveName + waveNumbre;
        pointsUI.text = pointName + "0";
        hpUI.text = "3" + hpName;
    }

    public void OverridePoints(int points)
    {
        pointsUI.text = pointName + points;
    }

    public void NextWaveNumbre()
    {
        currentWaveUI.text = waveName + (waveNumbre += 1);
    }

    public void OverrideHP(int hP)
    {
        hpUI.text = hP + hpName;
    }
}
