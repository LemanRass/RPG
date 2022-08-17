using System.Collections.Generic;
using Configs.Items;
using Configs.Items.Core;
using Enums;
using Inventory;
using Inventory.Data;

namespace Equipment
{
    public class UnitEquipment
    {
        private readonly Dictionary<EquipmentType, EquipmentSlot> _equipmentSlots;
        private readonly List<EquipmentType> _equipmentSlotsKeys;
        public EquipmentSlot this[EquipmentType equipmentType] => _equipmentSlots[equipmentType];
        public EquipmentSlot this[int index] => _equipmentSlots[_equipmentSlotsKeys[index]];
        public int count => _equipmentSlots.Count;
        
        public UnitEquipment(Unit owner)
        {
            _equipmentSlots = new Dictionary<EquipmentType, EquipmentSlot>();
            _equipmentSlotsKeys = new List<EquipmentType>();
            
            var slots = owner.GetComponentsInChildren<EquipmentSlot>();
            for (int i = 0; i < slots.Length; i++)
            {
                var slot = slots[i];
                _equipmentSlots.Add(slot.equipmentType, slot);
                _equipmentSlotsKeys.Add(slot.equipmentType);
            }
        }
        
        public void Equip(EquipmentItemData itemData)
        {
            var slot = _equipmentSlots[itemData.config.equipmentType];
            slot.Insert(itemData);
        }

        public void DeEquip(EquipmentSlot equipmentSlot)
        {
            equipmentSlot.Clear();
        }
        
        public void DropInventorySlot(InventorySlot from, EquipmentSlot to)
        {
            if (from.isEmpty)
                return;

            if (from.item is EquipmentItemData fromEquipment)
            {
                if (fromEquipment.config.equipmentType != to.equipmentType)
                    return;

                from.Insert(to.equipmentItem);
                to.Insert(fromEquipment);
            }
        }

        public void ApplyEquipment(StatType statType, ref float value)
        {
            for (int i = 0; i < _equipmentSlotsKeys.Count; i++)
            {
                var slot = _equipmentSlots[_equipmentSlotsKeys[i]];
                if (slot.isEmpty)
                    continue;

                for (int j = 0; j < slot.equipmentItem.config.stats.Count; j++)
                {
                    var stat = slot.equipmentItem.config.stats[j];

                    if (stat.statType != statType)
                        continue;

                    value += stat.value;
                }
            }
        }
    }
}