using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<QuestItemSO> items = new List<QuestItemSO>();

    private void OnEnable()
    {
        EventManager.OnTreasureChestUnlocked += HandleTreasureChestUnlocked;
    }

    private void OnDisable()
    {
        EventManager.OnTreasureChestUnlocked -= HandleTreasureChestUnlocked;
    }

    public void HandleTreasureChestUnlocked(QuestItemSO newItem)
    {
        items.Add(newItem);
    }

    public bool HasItem(QuestItemSO desiredItem)
    {
        bool itemFound = false;
        items.ForEach((QuestItemSO item) => 
        { 
            if (desiredItem.name == item.name) itemFound = true;
        });
        return itemFound;
    }

}
