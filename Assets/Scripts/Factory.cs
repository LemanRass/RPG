using System.Collections.Generic;
using Configs.Effects;
using Configs.Items;
using Data.Items;
using Effects.Core;
using Enums;
using UnityEngine;

public static class Factory
{
    private static readonly Dictionary<ItemType, ItemConfig> _items;
    private static readonly Dictionary<EffectType, EffectConfig> _effects;

    static Factory()
    {
        _items = new Dictionary<ItemType, ItemConfig>();
        foreach (var itemConfig in Resources.LoadAll<ItemConfig>("Configs/Items/"))
            _items.Add(itemConfig.type, itemConfig);
        
        Debug.Log($"Loaded: {_items.Count} items.");

        _effects = new Dictionary<EffectType, EffectConfig>();
        foreach (var effectConfig in Resources.LoadAll<EffectConfig>("Configs/Effects/"))
            _effects.Add(effectConfig.type, effectConfig);
        
        Debug.Log($"Loaded: {_effects.Count} effects.");
    }

    public static ItemData Create(ItemType itemType)
    {
        return _items[itemType].CreateInstance();
    }
    
    public static T Create<T>(ItemType itemType) where T : ItemData
    {
        return _items[itemType].CreateInstance() as T;
    }
    
    public static Effect Create(EffectType effectType)
    {
        return _effects[effectType].CreateInstance();
    }
    
    public static T Create<T>(EffectType effectType) where T : Effect
    {
        return _effects[effectType].CreateInstance() as T;
    }
}