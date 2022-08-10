using System.Collections.Generic;
using System.Linq;
using Configs.Items;

namespace Inventory
{
    public class UnitInventory
    {
        private readonly List<InventorySlot> _slots;
        public InventorySlot this[int index] => _slots[index];
        public int capacity => _slots.Count;
        public int itemsCount => _slots.Count(slot => !slot.isEmpty);

        public UnitInventory(int capacity)
        {
            _slots = new List<InventorySlot>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                _slots.Add(new InventorySlot());
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