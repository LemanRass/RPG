using System;
using Enums;
using UnityEngine;

namespace Configs.Items.Core
{
    [Serializable]
    public class ItemStat
    {
        public StatType statType;
        public float value;
    }
    
    public abstract class Item : ScriptableObject
    {
        public string name;
        public Sprite icon;
        public GameObject onGroundPrefab;
    }
}