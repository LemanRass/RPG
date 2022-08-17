using System.Collections.Generic;
using Configs.Items;
using Data.Items;
using Enums;
using UnityEngine;

public static class Factory
{
    private static readonly Dictionary<ItemType, ItemConfig> _items;

    static Factory()
    {
        _items = new Dictionary<ItemType, ItemConfig>();
        foreach (var itemConfig in Resources.LoadAll<ItemConfig>("Configs/Items/"))
            _items.Add(itemConfig.type, itemConfig);
        
        Debug.Log($"Loaded: {_items.Count} items.");
    }

    public static ItemData Create(ItemType itemType)
    {
        return _items[itemType].CreateInstance();
    }
    
    public static T Create<T>(ItemType itemType) where T : ItemData
    {
        return _items[itemType].CreateInstance() as T;
    }
}