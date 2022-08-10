using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Inventory
{
    public class UnitInventory : MonoBehaviour
    {
        public List<InventorySlot> slots;

        public InventorySlot FindFreeSlot()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].isEmpty)
                    return slots[i];
            }

            return null;
        }

        public int ItemsCount() => slots.Count(slot => !slot.isEmpty);
    }
}