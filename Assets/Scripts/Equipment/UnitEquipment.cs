using System.Collections.Generic;
using Configs.Items;
using Enums;
using UnityEngine;

namespace Equipment
{
    public class UnitEquipment : MonoBehaviour
    {
        public List<EquipmentSlot> equipmentSlots;
        public EquipmentSlot this[EquipmentType equipmentType] => GetSlot(equipmentType);

        public void Equip(Item item)
        {
            var slot = equipmentSlots.Find(slot => slot.type == item.type);
            slot.Insert(item);
        }

        public void DeEquip(EquipmentSlot equipmentSlot)
        {
            equipmentSlot.Clear();
        }

        private EquipmentSlot GetSlot(EquipmentType equipmentType)
        {
            return equipmentSlots.Find(n => n.type == equipmentType);
        }

        public void ApplyEquipment(StatType statType, ref float value)
        {
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                var slot = equipmentSlots[i];
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