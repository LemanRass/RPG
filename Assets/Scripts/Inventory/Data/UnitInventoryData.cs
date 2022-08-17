using System;
using System.Collections.Generic;
using Configs.Items;
using Enums;
using UnityEngine;

namespace Inventory.Data
{
    [Serializable]
    public class InventoryItemData
    {
        public ItemConfig config;
        public int count;
    }
    
    [CreateAssetMenu(fileName = "UnitInventoryData", menuName = "Unit/Data/UnitInventoryData")]
    public class UnitInventoryData : ScriptableObject
    {
        public List<InventoryItemData> items;
    }
}