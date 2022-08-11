using System.Collections.Generic;
using System.Linq;
using Configs.Items;
using Inventory.Data;

namespace Inventory
{
    public class UnitInventory
    {
        private readonly List<InventorySlot> _slots;
        public InventorySlot this[int index] => _slots[index];
        public int capacity => _slots.Count;
        public int itemsCount => _slots.Count(slot => !slot.isEmpty);

        public UnitInventory(UnitInventoryData inventoryData)
        {
            _slots = new List<InventorySlot>(inventoryData.items.Count);
            for (int i = 0; i < inventoryData.items.Count; i++)
            {
                _slots.Add(new InventorySlot(inventoryData.items[i]));
            }
        }
        
        public InventorySlot FindFreeSlot()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].isEmpty)
                    return _slots[i];
            }

            return null;
        }
    }
}