using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    public event UnityAction OnStartDefeated = () => { };
    [NonSerialized] public float healthPoints = 0f;
    [NonSerialized] public Slider sliderCmp;
    [SerializeField] private int potionCount = 1;
    [SerializeField] private float healAmount = 15f;
    private Animator animatorCmp;
    private BubbleEvent bubbleEventCmp;    
    private bool isDefeated = false; 

    private void Awake()
    {
        EventManager.UpdateChangePlayerPotions(potionCount);
        animatorCmp = GetComponentInChildren<Animator>();
        bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
        sliderCmp = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        bubbleEventCmp.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
    }

    private void Disable()
    {
        bubbleEventCmp.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
    }

    public void TakeDamage(float damageAmount)
    {
        healthPoints = Mathf.Max(healthPoints - damageAmount, 0);
        

        if (CompareTag(Constants.PLAYER_TAG))
        {
            EventManager.RaiseChangePlayerHealth(healthPoints);
        }

        if (sliderCmp != null)
        {
            sliderCmp.value = healthPoints;
        }

        if (healthPoints == 0f)
        {
            Defeated();
        }
        print(healthPoints + " points");
    }

    private void Defeated()
    {
        if (isDefeated) return;

        if (CompareTag(Constants.ENEMY_TAG))
        {
            OnStartDefeated.Invoke();
        }
        animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        isDefeated = true;
    }

    private void HandleBubbleCompleteDefeat()
    {
        Destroy(gameObject);
    }

    public void HandleHeal(InputAction.CallbackContext context)
    {
        if (!context.performed || potionCount == 0) return;
        
        potionCount--;
        healthPoints += healAmount;

        EventManager.RaiseChangePlayerHealth(healthPoints);
        EventManager.UpdateChangePlayerPotions(potionCount);
    }
}
