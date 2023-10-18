using System.Collections.Concurrent;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Quest Item",
    menuName = "RPG/Quest Item SO",
    order = 1
)]
public class QuestItemSO : ScriptableObject
{
    [Tooltip("Item name must be unique to prevent conflicts with other quest items.")]
    public string itemName;
}
