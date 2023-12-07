using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private Player player;
    private GameManager gameManager;
    private CanvasManager canvasManager;
    private ScoreUI scoreUI;
    
    private Button OK_Btn;
    private Button MaxHitPoints_btn;
    private Button Damage_btn;
    private Button CriticalHitDamage_btn;
    private Button PlayerScale_btn;
    private Button PlayerSpeed_btn;
    private Button CriticalHitChance_btn;
    private Button Luck_btn;
    private Button Loot_btn;

    private TextMeshProUGUI MaxHitPoints_COSTS_UI;
    private TextMeshProUGUI Damage_COSTS_UI;
    private TextMeshProUGUI CriticalHitDamage_COSTS_UI;
    private TextMeshProUGUI PlayerScale_COSTS_UI;
    private TextMeshProUGUI PlayerSpeed_COSTS_UI;
    private TextMeshProUGUI CriticalHitChance_COSTS_UI;
    private TextMeshProUGUI Luck_COSTS_UI;
    private TextMeshProUGUI Loot_COSTS_UI;

    public int maxHitPoints_COSTS = 1;
    public int damage_COSTS = 2;
    public int criticalHitDamage_COSTS = 3;
    public int playerScale_COSTS = 4;
    public int playerSpeed_COSTS = 5;
    public int criticalHitChance_COSTS = 6;
    public int luck_COSTS = 7;
    public int loot_COSTS = 8;

    private const string costs_LABEL = "Costs: ";
    
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
        OK_Btn = okButton.GetComponentInChildren<Button>();
        OK_Btn.onClick.AddListener(SetIsInUpgradePhase);
    }

    private void SetIsInUpgradePhase()
    {
        gameManager.IsInUpgradePhase = false;
    }
    
    private void AddListener_MaxHitPoints_Button()
    {
        Transform button = transform.Find("MaxHitPoints_btn");
        MaxHitPoints_btn = button.GetComponentInChildren<Button>();
        MaxHitPoints_btn.onClick.AddListener(AddMaxHitPoints);
        
        MaxHitPoints_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        MaxHitPoints_COSTS_UI.text = costs_LABEL + maxHitPoints_COSTS;
    }

    private void AddMaxHitPoints()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(maxHitPoints_COSTS)) return;
        
        // Add points to player.maxHitPoints
        player.currentHitPoints++;
        canvasManager.OverrideHitPoints(player.currentHitPoints);
        print("+1 HitPoint | Now: " + player.currentHitPoints);
    }
    
    private void AddListener_Damage_Button()
    {
        Transform button = transform.Find("Damage_btn");
        Damage_btn = button.GetComponentInChildren<Button>();
        Damage_btn.onClick.AddListener(AddDamage);
        
        Damage_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        Damage_COSTS_UI.text = costs_LABEL + damage_COSTS;
    }

    private void AddDamage()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(damage_COSTS)) return;
        
        // Add damage to player.damage
        player.currentDamage++;
        print("+1 Damage | Now: " + player.currentDamage);
    }
    
    private void AddListener_CriticalHitDamage_Button()
    {
        Transform button = transform.Find("CriticalHitDamage_btn");
        CriticalHitDamage_btn = button.GetComponentInChildren<Button>();
        CriticalHitDamage_btn.onClick.AddListener(AddCriticalHitDamage);
        
        CriticalHitDamage_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        CriticalHitDamage_COSTS_UI.text = costs_LABEL + criticalHitDamage_COSTS;
    }

    private void AddCriticalHitDamage()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(criticalHitDamage_COSTS)) return;
        
        // Add Damage to player.CriticalHitDamage
        player.currentCriticalHitDamage++;
        print("Add currentCriticalHitDamage | Now: " + player.currentCriticalHitDamage);
    }
    
    private void AddListener_PlayerScale_Button()
    {
        Transform button = transform.Find("PlayerScale_btn");
        PlayerScale_btn = button.GetComponentInChildren<Button>();
        PlayerScale_btn.onClick.AddListener(AddPlayerScale);
        
        PlayerScale_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        PlayerScale_COSTS_UI.text = costs_LABEL + playerScale_COSTS;
    }

    private void AddPlayerScale()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(playerScale_COSTS)) return;
        
        // Add Scale to player.PlayerScale
        print("Not Implemented Yet!");
    }
    
    private void AddListener_PlayerSpeed_Button()
    {
        Transform button = transform.Find("PlayerSpeed_btn");
        PlayerSpeed_btn = button.GetComponentInChildren<Button>();
        PlayerSpeed_btn.onClick.AddListener(AddPlayerSpeed);
        
        PlayerSpeed_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        PlayerSpeed_COSTS_UI.text = costs_LABEL + playerSpeed_COSTS;
    }

    private void AddPlayerSpeed()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(playerSpeed_COSTS)) return;
        
        // Add speed to player.PlayerSpeed
        player.currentPlayerSpeed++;
        print("Add PlayerSpeed | Now: " + player.currentPlayerSpeed);
    }
    
    private void AddListener_CriticalHitChance_Button()
    {
        Transform button = transform.Find("CriticalHitChance_btn");
        CriticalHitChance_btn = button.GetComponentInChildren<Button>();
        CriticalHitChance_btn.onClick.AddListener(AddCriticalHitChance);
        
        CriticalHitChance_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        CriticalHitChance_COSTS_UI.text = costs_LABEL + criticalHitChance_COSTS;
    }

    private void AddCriticalHitChance()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(criticalHitChance_COSTS)) return;
        
        // Add Chance to player.CriticalHitChance
        player.currentCriticalHitChance += 0.05f;
        print("Add CriticalHitChance | Now: " + player.currentCriticalHitChance);
    }
    
    private void AddListener_Luck_Button()
    {
        Transform button = transform.Find("Luck_btn");
        Luck_btn = button.GetComponentInChildren<Button>();
        Luck_btn.onClick.AddListener(AddLuck);
        
        Luck_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        Luck_COSTS_UI.text = costs_LABEL + luck_COSTS;
    }

    private void AddLuck()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(luck_COSTS)) return;
        
        // Add luck to player.Luck
        player.currentLuck += 0.05f;
        print("Add Luck | Now: " + player.currentLuck);
    }
    
    private void AddListener_Loot_Button()
    {
        Transform button = transform.Find("Loot_btn");
        Loot_btn = button.GetComponentInChildren<Button>();
        Loot_btn.onClick.AddListener(AddLoot);
        
        Loot_COSTS_UI = button.Find("Costs").GetComponentInChildren<TextMeshProUGUI>();
        Loot_COSTS_UI.text = costs_LABEL + loot_COSTS;
    }

    private void AddLoot()
    {
        if (!scoreUI.DoPlayerHaveEnoughPoints(loot_COSTS)) return;
        
        // Add loot to player.Loot
        player.currentLoot += 0.05f;
        print("Add Loot | Now: " + player.currentLoot);
    }
}
