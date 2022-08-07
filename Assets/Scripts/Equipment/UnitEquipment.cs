using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Equipment
{
    public class UnitEquipment : MonoBehaviour
    {
        public List<EquipmentSlot> equipmentSlots;

        public void Equip(Item item)
        {
            var slot = equipmentSlots.Find(slot => slot.type == item.type);

            if (slot.transform != null)
            {
                DeEquip(slot);
            }

            var itemObject = Instantiate(item, slot.transform);
            itemObject.transform.localPosition = Vector3.zero;
            itemObject.transform.localRotation = Quaternion.identity;
            itemObject.transform.localScale = Vector3.one;
        }

        public void DeEquip(EquipmentSlot slot)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}