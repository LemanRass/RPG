using System;
using System.Collections.Generic;
using Configs.Items.Core;
using UnityEngine;

namespace Inventory.Data
{
    public abstract class ItemData
    {
        public readonly ItemConfig config;
        public int count;

        public ItemData(ItemConfig config)
        {
            this.config = config;
        }
    }

    public class EquipmentItemData : ItemData
    {
        public new readonly EquipmentItemConfig config;
        public int durability;

        public EquipmentItemData(EquipmentItemConfig config) : base(config)
        {
            this.config = config;
        }
    }

    public class ResourceItemData : ItemData
    {
        public new readonly ResourceItemConfig config;

        public ResourceItemData(ResourceItemConfig config) : base(config)
        {
            this.config = config;
        }
    }

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