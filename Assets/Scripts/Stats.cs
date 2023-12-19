using System;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private Player player;
    
    public TextMeshProUGUI LifePoints;
    public TextMeshProUGUI Speed;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI CriticalHitDamage;
    public TextMeshProUGUI CriticalHitChance;
    public TextMeshProUGUI Luck;
    public TextMeshProUGUI Loot;

    private const string LABEL_lifePoints = "Life Points: ";
    private const string LABEL_speed = "Speed: ";
    private const string LABEL_damage = "Damage: ";
    private const string LABEL_criticalHitDamage = "Crit. Damage: ";
    private const string LABEL_criticalHitChance = "Crit. Chance: ";
    private const string LABEL_luck = "Drop Chance: ";
    private const string LABEL_loot = "Points per Brick: +";

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        UpdateStats();
    }

    public void UpdateStats()
    {
        LifePoints.text = LABEL_lifePoints + player.currentHitPoints;
        Speed.text = LABEL_speed + player.currentPlayerSpeed;
        Damage.text = LABEL_damage + player.currentDamage;
        CriticalHitDamage.text = LABEL_criticalHitDamage + player.currentCriticalHitDamage;
        CriticalHitChance.text = LABEL_criticalHitChance + player.currentCriticalHitChance + "%";
        Luck.text = LABEL_luck + player.currentLuck + "%";
        Loot.text = LABEL_loot + player.currentLoot;
    }
}
