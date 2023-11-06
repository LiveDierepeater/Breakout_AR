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
        currentWaveUI.text = waveName + waveNumbre.ToString();
        pointsUI.text = pointName + "0";
        hpUI.text = "3" + hpName;
    }

    public void OverridePoints(int points)
    {
        pointsUI.text = pointName + points;
    }

    public void SetWaveNumbre(int currentWave)
    {

    }

    public void OverrideHP(int hP)
    {

    }
}
