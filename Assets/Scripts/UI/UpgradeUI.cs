using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private Player player;
    private GameManager gameManager;
    private CanvasManager canvasManager;
    private ScoreUI scoreUI;
    
    private Button okBtn;
    private Button maxHitPointsBtn;
    private Button damageBtn;
    private Button criticalHitDamageBtn;
    private Button playerScaleBtn;
    private Button playerSpeedBtn;
    private Button criticalHitChanceBtn;
    private Button luckBtn;
    private Button lootBtn;

    private TextMeshProUGUI maxHitPointsCostsUI;
    private TextMeshProUGUI damageCostsUI;
    private TextMeshProUGUI criticalHitDamageCostsUI;
    private TextMeshProUGUI playerScaleCostsUI;
    private TextMeshProUGUI playerSpeedCostsUI;
    private TextMeshProUGUI criticalHitChanceCostsUI;
    private TextMeshProUGUI luckCostsUI;
    private TextMeshProUGUI lootCostsUI;

    [FormerlySerializedAs("maxHitPoints_COSTS")] public int maxHitPointsCosts = 1;
    [FormerlySerializedAs("damage_COSTS")] public int damageCosts = 2;
    [FormerlySerializedAs("criticalHitDamage_COSTS")] public int criticalHitDamageCosts = 3;
    [FormerlySerializedAs("playerScale_COSTS")] public int playerScaleCosts = 4;
    [FormerlySerializedAs("playerSpeed_COSTS")] public int playerSpeedCosts = 5;
    [FormerlySerializedAs("criticalHitChance_COSTS")] public int criticalHitChanceCosts = 6;
    [FormerlySerializedAs("luck_COSTS")] public int luckCosts = 7;
    [FormerlySerializedAs("loot_COSTS")] public int lootCosts = 8;

    private const string costsLabel = "Costs: ";
    
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        scoreUI = canvasManager.transform.GetComponentInChildren<ScoreUI>();
        gameManager.OnUpgradePhaseActive += SWITCH_UpgradeUI;
        gameObject.SetActive(false);

        AddListenerToAllUpgradeUIButtons();
    }
    
    private void SWITCH_UpgradeUI(bool isActive)
    {
        switch (isActive)
        {
            case true:
                gameObject.SetActive(true);
                break;
            
            case false:
                gameObject.SetActive(false);
                break;
        }
    }

    private void AddListenerToAllUpgradeUIButtons()
    {
        AddListener_OK_Button();
        AddListener_MaxHitPoints_Button();
        AddListener_Damage_Button();
        AddListener_CriticalHitDamage_Button();
        AddListener_PlayerScale_Button();
        AddListener_PlayerSpeed_Button();
        AddListener_CriticalHitChance_Button();
        AddListener_Luck_Button();
        AddListener_Loot_Button();
    }
    
    private void AddListener_OK_Button()
    {
        Transform okButton = transform.Find("OK_btn");
        okBtn = okButton.GetComponentInChildren<Button>();
        okBtn.onClick.AddListener(SetIsInUpgradePhase);
    }

    private void SetIsInUpgradePhase()
    {
        gameManager.IsInUpgradePhase = false;
    }
    
    private void AddListener_MaxHitPoints_Button()
    {
        Transform button = transform.Find("MaxHitPoints_btn");
        maxHitPointsBtn = button.GetComponentInChildren<Button>();
        maxHitPointsBtn.onClick.AddListener(AddMaxHitPoints);
        
        maxHitPointsCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        maxHitPointsCostsUI.text = costsLabel + maxHitPointsCosts;
    }

    private void AddMaxHitPoints()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(maxHitPointsCosts)) return;
        
        // Add points to player.maxHitPoints
        player.currentHitPoints++;
        canvasManager.OverrideHitPoints(player.currentHitPoints);
        print("+1 HitPoint | Now: " + player.currentHitPoints);
    }
    
    private void AddListener_Damage_Button()
    {
        Transform button = transform.Find("Damage_btn");
        damageBtn = button.GetComponentInChildren<Button>();
        damageBtn.onClick.AddListener(AddDamage);
        
        damageCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        damageCostsUI.text = costsLabel + damageCosts;
    }

    private void AddDamage()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(damageCosts)) return;
        
        // Add damage to player.damage
        player.currentDamage++;
        print("+1 Damage | Now: " + player.currentDamage);
    }
    
    private void AddListener_CriticalHitDamage_Button()
    {
        Transform button = transform.Find("CriticalHitDamage_btn");
        criticalHitDamageBtn = button.GetComponentInChildren<Button>();
        criticalHitDamageBtn.onClick.AddListener(AddCriticalHitDamage);
        
        criticalHitDamageCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        criticalHitDamageCostsUI.text = costsLabel + criticalHitDamageCosts;
    }

    private void AddCriticalHitDamage()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(criticalHitDamageCosts)) return;
        
        // Add Damage to player.CriticalHitDamage
        player.currentCriticalHitDamage++;
        print("Add currentCriticalHitDamage | Now: " + player.currentCriticalHitDamage);
    }
    
    private void AddListener_PlayerScale_Button()
    {
        Transform button = transform.Find("PlayerScale_btn");
        playerScaleBtn = button.GetComponentInChildren<Button>();
        playerScaleBtn.onClick.AddListener(AddPlayerScale);
        
        playerScaleCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        playerScaleCostsUI.text = costsLabel + playerScaleCosts;
    }

    private void AddPlayerScale()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(playerScaleCosts)) return;
        
        // Add Scale to player.PlayerScale
        print("Not Implemented Yet!");
    }
    
    private void AddListener_PlayerSpeed_Button()
    {
        Transform button = transform.Find("PlayerSpeed_btn");
        playerSpeedBtn = button.GetComponentInChildren<Button>();
        playerSpeedBtn.onClick.AddListener(AddPlayerSpeed);
        
        playerSpeedCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        playerSpeedCostsUI.text = costsLabel + playerSpeedCosts;
    }

    private void AddPlayerSpeed()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(playerSpeedCosts)) return;
        
        // Add speed to player.PlayerSpeed
        player.currentPlayerSpeed++;
        print("Add PlayerSpeed | Now: " + player.currentPlayerSpeed);
    }
    
    private void AddListener_CriticalHitChance_Button()
    {
        Transform button = transform.Find("CriticalHitChance_btn");
        criticalHitChanceBtn = button.GetComponentInChildren<Button>();
        criticalHitChanceBtn.onClick.AddListener(AddCriticalHitChance);
        
        criticalHitChanceCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        criticalHitChanceCostsUI.text = costsLabel + criticalHitChanceCosts;
    }

    private void AddCriticalHitChance()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(criticalHitChanceCosts)) return;
        
        // Add Chance to player.CriticalHitChance
        player.currentCriticalHitChance += 0.05f;
        print("Add CriticalHitChance | Now: " + player.currentCriticalHitChance);
    }
    
    private void AddListener_Luck_Button()
    {
        Transform button = transform.Find("Luck_btn");
        luckBtn = button.GetComponentInChildren<Button>();
        luckBtn.onClick.AddListener(AddLuck);
        
        luckCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        luckCostsUI.text = costsLabel + luckCosts;
    }

    private void AddLuck()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(luckCosts)) return;
        
        // Add luck to player.Luck
        player.currentLuck += 0.05f;
        print("Add Luck | Now: " + player.currentLuck);
    }
    
    private void AddListener_Loot_Button()
    {
        Transform button = transform.Find("Loot_btn");
        lootBtn = button.GetComponentInChildren<Button>();
        lootBtn.onClick.AddListener(AddLoot);
        
        lootCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        lootCostsUI.text = costsLabel + lootCosts;
    }

    private void AddLoot()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(lootCosts)) return;
        
        // Add loot to player.Loot
        player.currentLoot += 0.05f;
        print("Add Loot | Now: " + player.currentLoot);
    }
}
