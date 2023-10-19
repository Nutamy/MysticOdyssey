using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPCController : MonoBehaviour
{
    public TextAsset inkJSON;
    public QuestItemSO desiredQuestItem;
    private Canvas canvasCmp;
    private Reward rewardCmp;
    public bool hasQuestItem = false;

    private void Awake()
    {
        canvasCmp = GetComponentInChildren<Canvas>();
        rewardCmp = GetComponent<Reward>();
    }

    private void OnTriggerEnter()
    {
        canvasCmp.enabled = true;
    }

    private void OnTriggerExit()
    {
        canvasCmp.enabled = false;
    }

    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (!context.performed || !canvasCmp.enabled) return;
        if (inkJSON == null)
        {
            Debug.LogWarning("Please add an ink file to the npc.");
            return;
        }
        else
        {
            
        }
        EventManager.RaiseTreasureChestUnlocked(inkJSON, gameObject);
    }

    public bool CheckPlayerForQuestItem()
    {
        if(hasQuestItem) return true;
        Inventory inventoryCmp = GameObject.FindGameObjectWithTag(
            Constants.PLAYER_TAG
        ).GetComponent<Inventory>();
        hasQuestItem = inventoryCmp.HasItem(desiredQuestItem);
        if (rewardCmp != null && hasQuestItem)
        {
            rewardCmp.SendReward();
        }
        return hasQuestItem;
     }

}
