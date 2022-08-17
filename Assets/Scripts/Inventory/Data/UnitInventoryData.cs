using System;
using System.Collections.Generic;
using Configs.Items.Core;
using UnityEngine;

namespace Inventory.Data
{
    public abstract class ItemData
    {
        public event Action onChanged;
        
        public readonly ItemConfig config;

        private int _count;
        public int count
        {
            get => _count;
            set
            {
                _count = value;
                OnChanged();
            }
        }

        public ItemData(ItemConfig config)
        {
            this.config = config;
        }

        protected void OnChanged()
        {
            onChanged?.Invoke();
        }
    }

    public class EquipmentItemData : ItemData
    {
        public new readonly EquipmentItemConfig config;

        private int _durability;
        public int durability
        {
            get => _durability;
            set
            {
                _durability = value;
                OnChanged();
            }
        }

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