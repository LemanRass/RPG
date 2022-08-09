using System.Collections.Generic;
using Configs.Items;
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
    }
}