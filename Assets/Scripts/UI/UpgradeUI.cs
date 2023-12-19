using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public PointsUI pointsUI;
    
    private Player player;
    private GameManager gameManager;
    private CanvasManager canvasManager;
    private SoundManager soundManager;
    
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

    [FormerlySerializedAs("maxHitPoints_COSTS")] public int maxHitPointsCosts = 10;
    [FormerlySerializedAs("damage_COSTS")] public int damageCosts = 5;
    [FormerlySerializedAs("criticalHitDamage_COSTS")] public int criticalHitDamageCosts = 3;
    [FormerlySerializedAs("playerScale_COSTS")] public int playerScaleCosts = 5;
    [FormerlySerializedAs("playerSpeed_COSTS")] public int playerSpeedCosts = 5;
    [FormerlySerializedAs("criticalHitChance_COSTS")] public int criticalHitChanceCosts = 8;
    [FormerlySerializedAs("luck_COSTS")] public int luckCosts = 8;
    [FormerlySerializedAs("loot_COSTS")] public int lootCosts = 50;
    
    private int currentMaxHitPointsCosts ;
    private int currentDamageCosts;
    private int currentCriticalHitDamageCosts;
    private int currentPlayerScaleCosts;
    private int currentPlayerSpeedCosts;
    private int currentCriticalHitChanceCosts;
    private int currentLuckCosts;
    private int currentLootCosts;

    private const string costsLabel = "Costs: ";
    
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnUpgradePhaseActive += SWITCH_UpgradeUI;
        gameObject.SetActive(false);
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        currentMaxHitPointsCosts = maxHitPointsCosts;
        currentDamageCosts = damageCosts;
        currentCriticalHitDamageCosts = criticalHitDamageCosts;
        currentPlayerScaleCosts = playerScaleCosts;
        currentPlayerSpeedCosts = playerSpeedCosts;
        currentCriticalHitChanceCosts = criticalHitChanceCosts;
        currentLuckCosts = luckCosts;
        currentLootCosts = lootCosts;
        
        AddListenerToAllUpgradeUIButtons();
    }

    private void SWITCH_UpgradeUI(bool isActive)
    {
        switch (isActive)
        {
            case true: // game is paused
                gameObject.SetActive(true);
                soundManager.DampSound();
                player.lightingStrike.gameObject.SetActive(false); // Deactivates Lighting Strike Attack
                break;
            
            case false: // game goes on
                gameObject.SetActive(false);
                player.anchored = false;
                soundManager.NormalizeSound();
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
        maxHitPointsCostsUI.text = costsLabel + currentMaxHitPointsCosts;
    }

    private void AddMaxHitPoints() // IMPLEMENTED
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentMaxHitPointsCosts)) return;
        
        // Add points to player.maxHitPoints
        player.currentHitPoints++;
        canvasManager.OverrideHitPoints(player.currentHitPoints);
        print("+1 HitPoint | Now: " + player.currentHitPoints);
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentMaxHitPointsCosts += maxHitPointsCosts;
        maxHitPointsCostsUI.text = costsLabel + currentMaxHitPointsCosts;
    }
    
    private void AddListener_Damage_Button()
    {
        Transform button = transform.Find("Damage_btn");
        damageBtn = button.GetComponentInChildren<Button>();
        damageBtn.onClick.AddListener(AddDamage);
        
        damageCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        damageCostsUI.text = costsLabel + currentDamageCosts;
    }

    private void AddDamage() // IMPLEMENTED
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentDamageCosts)) return;
        
        // Add damage to player.damage
        player.currentDamage++;
        print("+1 Damage | Now: " + player.currentDamage);
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentDamageCosts += damageCosts;
        damageCostsUI.text = costsLabel + currentDamageCosts;
    }
    
    private void AddListener_CriticalHitDamage_Button()
    {
        Transform button = transform.Find("CriticalHitDamage_btn");
        criticalHitDamageBtn = button.GetComponentInChildren<Button>();
        criticalHitDamageBtn.onClick.AddListener(AddCriticalHitDamage);
        
        criticalHitDamageCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        criticalHitDamageCostsUI.text = costsLabel + currentCriticalHitDamageCosts;
    }

    private void AddCriticalHitDamage() // IMPLEMENTED
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentCriticalHitDamageCosts)) return;
        
        // Add Damage to player.CriticalHitDamage
        player.currentCriticalHitDamage++;
        print("Add currentCriticalHitDamage | Now: " + player.currentCriticalHitDamage);
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentCriticalHitDamageCosts += criticalHitDamageCosts;
        criticalHitDamageCostsUI.text = costsLabel + currentCriticalHitDamageCosts;
    }
    
    private void AddListener_PlayerScale_Button()
    {
        Transform button = transform.Find("PlayerScale_btn");
        playerScaleBtn = button.GetComponentInChildren<Button>();
        playerScaleBtn.onClick.AddListener(AddPlayerScale);
        
        playerScaleCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        playerScaleCostsUI.text = costsLabel + currentPlayerScaleCosts;
    }

    private void AddPlayerScale() // TODO: IMPLEMENT
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentPlayerScaleCosts)) return;
        
        // Add Scale to player.PlayerScale
        print("Not Implemented Yet!");
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentPlayerScaleCosts += playerScaleCosts;
        playerScaleCostsUI.text = costsLabel + currentPlayerScaleCosts;
    }
    
    private void AddListener_PlayerSpeed_Button()
    {
        Transform button = transform.Find("PlayerSpeed_btn");
        playerSpeedBtn = button.GetComponentInChildren<Button>();
        playerSpeedBtn.onClick.AddListener(AddPlayerSpeed);
        
        playerSpeedCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        playerSpeedCostsUI.text = costsLabel + currentPlayerSpeedCosts;
    }

    private void AddPlayerSpeed() // IMPLEMENTED
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentPlayerSpeedCosts)) return;
        
        // Add speed to player.PlayerSpeed
        player.currentPlayerSpeed += 2f;
        print("Add PlayerSpeed | Now: " + player.currentPlayerSpeed);
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentPlayerSpeedCosts += playerSpeedCosts;
        playerSpeedCostsUI.text = costsLabel + currentPlayerSpeedCosts;
    }
    
    private void AddListener_CriticalHitChance_Button()
    {
        Transform button = transform.Find("CriticalHitChance_btn");
        criticalHitChanceBtn = button.GetComponentInChildren<Button>();
        criticalHitChanceBtn.onClick.AddListener(AddCriticalHitChance);
        
        criticalHitChanceCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        criticalHitChanceCostsUI.text = costsLabel + currentCriticalHitChanceCosts;
    }

    private void AddCriticalHitChance() // IMPLEMENTED
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentCriticalHitChanceCosts)) return;
        
        // Add Chance to player.CriticalHitChance
        player.currentCriticalHitChance += 0.05f;
        print("Add CriticalHitChance | Now: " + player.currentCriticalHitChance);
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentCriticalHitChanceCosts += criticalHitChanceCosts;
        criticalHitChanceCostsUI.text = costsLabel + currentCriticalHitChanceCosts;
    }
    
    private void AddListener_Luck_Button()
    {
        Transform button = transform.Find("Luck_btn");
        luckBtn = button.GetComponentInChildren<Button>();
        luckBtn.onClick.AddListener(AddLuck);
        
        luckCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        luckCostsUI.text = costsLabel + currentLuckCosts;
    }

    private void AddLuck() // IMPLEMENTED
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentLuckCosts)) return;
        
        // Add luck to player.Luck
        // In PowerUpSpawner.cs in Awake() the Spawner will get a ref to the player and grabs the "currentLuck" value and adds it to his "standardSpawnChance".
        player.currentLuck += 0.05f;
        print("Add Luck | Now: " + player.currentLuck);
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentLuckCosts += luckCosts;
        luckCostsUI.text = costsLabel + currentLuckCosts;
    }
    
    private void AddListener_Loot_Button()
    {
        Transform button = transform.Find("Loot_btn");
        lootBtn = button.GetComponentInChildren<Button>();
        lootBtn.onClick.AddListener(AddLoot);
        
        lootCostsUI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        lootCostsUI.text = costsLabel + currentLootCosts;
    }

    private void AddLoot() // IMPLEMENTED
    {
        if (!pointsUI.DoPlayerHaveEnoughPoints(currentLootCosts)) return;
        
        // Add loot to player.Loot
        player.currentLoot++;
        print("Add Loot | Now: " + player.currentLoot);
        
        // Sound
        soundManager.PlayBuySound();
        
        // Higher Costs
        currentLootCosts += lootCosts;
        lootCostsUI.text = costsLabel + currentLootCosts;
    }
}
