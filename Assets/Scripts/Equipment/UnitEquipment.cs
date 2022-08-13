using System.Collections.Generic;
using Configs.Items;
using Configs.Items.Core;
using Enums;
using Inventory;

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
        
        public void Equip(IEquipment item)
        {
            var slot = _equipmentSlots[item.equipmentType];
            slot.Insert(item);
        }

        public void DeEquip(EquipmentSlot equipmentSlot)
        {
            equipmentSlot.Clear();
        }

        public void DropInventorySlot(InventorySlot from, EquipmentSlot to)
        {
            var fromItem = from.item;
            var toItem = (Item)to.item;

            if (fromItem is IEquipment equipment)
            {
                if (equipment.equipmentType == to.equipmentType)
                {
                    to.Insert(equipment);
                    from.Insert(toItem);
                }
            }
        }

        public void ApplyEquipment(StatType statType, ref float value)
        {
            for (int i = 0; i < _equipmentSlotsKeys.Count; i++)
            {
                var slot = _equipmentSlots[_equipmentSlotsKeys[i]];
                if (slot.isEmpty)
                    continue;

                for (int j = 0; j < slot.item.stats.Count; j++)
                {
                    var stat = slot.item.stats[j];

                    if (stat.statType != statType)
                        continue;

                    value += stat.value;
                }
            }
        }
    }
}