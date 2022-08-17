using System;
using Data.Items;
using Enums;
using UnityEngine;

namespace Configs.Items
{
    [Serializable]
    public class ItemStat
    {
        public StatType statType;
        public float value;
    }
    
    public abstract class ItemConfig : ScriptableObject
    {
        public ItemType type;
        public string name;
        public Sprite icon;
        public GameObject onGroundPrefab;
        public int maxCount;

        public abstract ItemData CreateInstance();
    }
}