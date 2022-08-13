using System.Collections.Generic;
using System.Linq;
using Configs.Items;
using Configs.Items.Core;
using Equipment;
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

        public void DropInventorySlot(InventorySlot from, InventorySlot to)
        {
            var fromItem = from.item;
            var toItem = to.item;
        
            from.Insert(toItem);
            to.Insert(fromItem);
        }

        public void DropEquipmentSlot(EquipmentSlot from, InventorySlot to)
        {
            var fromItem = (Item)from.item;
            var toItem = to.item;
        
            from.Insert((IEquipment)toItem);
            to.Insert(fromItem);
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