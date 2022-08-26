using System;
using System.Collections.Generic;
using Data.Items;
using Enums;

namespace Equipment
{
    public class UnitEquipment
    {
        private readonly Dictionary<EquipmentType, EquipmentSlot> _equipmentSlots;
        private readonly List<EquipmentType> _equipmentSlotsKeys;
        public EquipmentSlot this[EquipmentType equipmentType] => _equipmentSlots[equipmentType];
        public EquipmentSlot this[int index] => _equipmentSlots[_equipmentSlotsKeys[index]];
        public int count => _equipmentSlots.Count;

        public event Action onEquipmentChanged; 

        public UnitEquipment(Unit unit)
        {
            _equipmentSlots = new Dictionary<EquipmentType, EquipmentSlot>();
            _equipmentSlotsKeys = new List<EquipmentType>();
            
            var slots = unit.GetComponentsInChildren<EquipmentSlot>();
            for (int i = 0; i < slots.Length; i++)
            {
                var slot = slots[i];
                _equipmentSlots.Add(slot.equipmentType, slot);
                _equipmentSlotsKeys.Add(slot.equipmentType);
            }
        }
        
        public void EquipItem(EquipmentItemData itemData)
        {
            var slot = _equipmentSlots[itemData.config.equipmentType];
            slot.Insert(itemData);
            onEquipmentChanged?.Invoke();
        }

        public void ClearSlot(EquipmentSlot equipmentSlot)
        {
            equipmentSlot.Clear();
            onEquipmentChanged?.Invoke();
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