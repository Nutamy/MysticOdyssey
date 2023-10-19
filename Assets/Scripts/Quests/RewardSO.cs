using System.Collections.Concurrent;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Reward",
    menuName = "RPG/Reward SO",
    order = 2
)]
public class RewardSO : ScriptableObject
{
    public float bonusHealth = 0f;
    public float bonusDamage = 0f;
    public int bonusPotions = 0;
    public bool forceWeaponSwap = false;
    public Weapons weapon = Weapons.Sword;
}
