using System.ComponentModel;
using UnityEngine;

namespace PlayerData
{
    [CreateAssetMenu(menuName = "Data/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int defaultMaxHitPoints = 5;
        [SerializeField] private int defaultDamage = 1;
        [SerializeField] private int defaultCriticalHitDamage = 3;
        [SerializeField] private Vector3 defaultPlayerScale = Vector3.one;
        [SerializeField] private float defaultSpeed = 15f;
        [SerializeField] private float defaultCriticalHitChance = 0.05f;
        [SerializeField] private float defaultLuck = 0.05f;
        [SerializeField] private float defaultLoot = 1f;
        
        [ReadOnly(true)] public int DefaultMaxHitPoints => defaultMaxHitPoints;
        [ReadOnly(true)] public int DefaultDamage => defaultDamage;
        [ReadOnly(true)] public int DefaultCriticalHitDamage => defaultCriticalHitDamage;
        [ReadOnly(true)] public Vector3 DefaultPlayerScale => defaultPlayerScale;
        [ReadOnly(true)] public float DefaultSpeed => defaultSpeed;
        [ReadOnly(true)] public float DefaultCriticalHitChance => defaultCriticalHitChance;
        [ReadOnly(true)] public float DefaultLuck => defaultLuck;
        [ReadOnly(true)] public float DefaultLoot => defaultLoot;
        
        // Unfinished Example:
        [SerializeField] private Sprite[] sprites;

        public Sprite GetSprite(int size)
        {
            return sprites[size];
        }

        public void ApplyResize(Player playerRef, Powerup powerupRef)
        {
            
        }
    }
}
