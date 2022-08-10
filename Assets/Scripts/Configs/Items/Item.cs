using System;
using System.Collections.Generic;
using Enums;
using Equipment;
using UnityEngine;

namespace Configs.Items
{
    [Serializable]
    public class ItemStat
    {
        public StatType statType;
        public float value;
    }
    
    public abstract class Item : ScriptableObject
    {
        public EquipmentType equipmentType;
        public GameObject onUnitPrefab;
        public GameObject onGroundPrefab;
        public List<ItemStat> stats;
    }
}