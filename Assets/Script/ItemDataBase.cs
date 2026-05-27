using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<PocketItem> items;

    public PocketItem GetItem(int index)
    {
        return items[index];
    }
}