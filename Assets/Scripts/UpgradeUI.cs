using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private Player player;
    private GameManager gameManager;
    
    private Button OK_Btn;
    private Button MaxHitPoints_btn;
    private Button Damage_btn;
    private Button CriticalHitDamage_btn;
    private Button PlayerScale_btn;
    private Button PlayerSpeed_btn;
    private Button CriticalHitChance_btn;
    private Button Luck_btn;
    private Button Loot_btn;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                // TODO: Fill with actions that will happen when OnUpgradePhaseActive gets false / Player pressed OK_btn.
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
    }

    private void AddMaxHitPoints()
    {
        // Add points to player.maxHitPoints
        print("AddMaxHitPoints");
    }
    
    private void AddListener_Damage_Button()
    {
        Transform button = transform.Find("Damage_btn");
        Damage_btn = button.GetComponentInChildren<Button>();
        Damage_btn.onClick.AddListener(AddDamage);
    }

    private void AddDamage()
    {
        // Add damage to player.damage
        print("AddDamage");
    }
    
    private void AddListener_CriticalHitDamage_Button()
    {
        Transform button = transform.Find("CriticalHitDamage_btn");
        CriticalHitDamage_btn = button.GetComponentInChildren<Button>();
        CriticalHitDamage_btn.onClick.AddListener(AddCriticalHitDamage);
    }

    private void AddCriticalHitDamage()
    {
        // Add Damage to player.CriticalHitDamage
        print("AddCriticalHitDamage");
    }
    
    private void AddListener_PlayerScale_Button()
    {
        Transform button = transform.Find("PlayerScale_btn");
        PlayerScale_btn = button.GetComponentInChildren<Button>();
        PlayerScale_btn.onClick.AddListener(AddPlayerScale);
    }

    private void AddPlayerScale()
    {
        // Add Scale to player.PlayerScale
        print("AddPlayerScale");
    }
    
    private void AddListener_PlayerSpeed_Button()
    {
        Transform button = transform.Find("PlayerSpeed_btn");
        PlayerSpeed_btn = button.GetComponentInChildren<Button>();
        PlayerSpeed_btn.onClick.AddListener(AddPlayerSpeed);
    }

    private void AddPlayerSpeed()
    {
        // Add speed to player.PlayerSpeed
        print("AddPlayerSpeed");
    }
    
    private void AddListener_CriticalHitChance_Button()
    {
        Transform button = transform.Find("CriticalHitChance_btn");
        CriticalHitChance_btn = button.GetComponentInChildren<Button>();
        CriticalHitChance_btn.onClick.AddListener(AddCriticalHitChance);
    }

    private void AddCriticalHitChance()
    {
        // Add Chance to player.CriticalHitChance
        print("AddCriticalHitChance");
    }
    
    private void AddListener_Luck_Button()
    {
        Transform button = transform.Find("Luck_btn");
        Luck_btn = button.GetComponentInChildren<Button>();
        Luck_btn.onClick.AddListener(AddLuck);
    }

    private void AddLuck()
    {
        // Add luck to player.Luck
        print("AddLuck");
    }
    
    private void AddListener_Loot_Button()
    {
        Transform button = transform.Find("Loot_btn");
        Loot_btn = button.GetComponentInChildren<Button>();
        Loot_btn.onClick.AddListener(AddLoot);
    }

    private void AddLoot()
    {
        // Add loot to player.Loot
        print("AddLoot");
    }
}
