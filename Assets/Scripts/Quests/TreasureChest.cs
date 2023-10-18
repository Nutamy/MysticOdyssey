using UnityEngine;
using UnityEngine.InputSystem;

public class TreasureChest : MonoBehaviour
{
    public QuestItemSO questItem;
    public Animator animatorCmp;
    private bool isInteractable = false;
    private bool hasBeenOpened = false;
    private void OnTriggerEnter()
    {
        isInteractable = true;
    }

    private void OnTriggerExit()
    {
        isInteractable = false;
    }

    public void HandleInteract(InputAction.CallbackContext context)
    {
        if(!isInteractable || hasBeenOpened || !context.performed) return;
        hasBeenOpened = true;
        animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
        EventManager.RaiseOpeningTreasureChest(questItem);
    }
}
