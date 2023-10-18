using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterStatsSO stats;
    private Health healthCmp;
    private Combat combatCmp;

    private void Awake()
    {
        if (stats == null)
        {
            Debug.LogWarning($"{name} has no CharacterStatsSO assigned to it.");
        }
        healthCmp = GetComponent<Health>();
        combatCmp = GetComponent<Combat>();
    }

    private void Start()
    {
        healthCmp.healthPoints = stats.health;
        combatCmp.damage = stats.damage;

        EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
    }
}
