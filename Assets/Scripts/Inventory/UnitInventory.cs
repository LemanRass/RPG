using System.Collections.Generic;
using System.Linq;
using Data.Items;
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
                var inventoryItem = inventoryData.items[i];
                if (inventoryItem.config == null)
                {
                    _slots.Add(new InventorySlot(null));
                }
                else
                {
                    var item = Factory.Create(inventoryItem.config.type);
                    item.count = inventoryItem.count;
                    _slots.Add(new InventorySlot(item));
                }
            }
        }

        public void Move(InventorySlot from, InventorySlot to)
        {
            if (from == to)
                return;
            
            if (from.isEmpty)
                return;

            if (!to.isEmpty)
            {
                if (from.item.config.type == to.item.config.type)
                {
                    Merge(from, to);
                    return;
                }
            }
            
            Swap(from, to);
        }
        
        public void Split(InventorySlot from, InventorySlot to, int count)
        {
            if (from == to)
                return;
            
            if (from.isEmpty)
                return;

            if (from.item.count < count)
                return;

            
            if (to.isEmpty)
            {
                var fromItem = from.item;
                var toItem = Factory.Create(fromItem.config.type);
                InsertItem(to, toItem);
            }

            int toItemTotalCount = to.item.count + count;
            if (toItemTotalCount > to.item.config.maxCount)
            {
                int diff = toItemTotalCount - to.item.config.maxCount;
                count -= diff;
            }
            
            from.item.count -= count;
            to.item.count += count;
                
            if (from.item.count < 1)
                ClearSlot(from);
        }

        
        private void Swap(InventorySlot from, InventorySlot to)
        {
            var fromItem = from.item;
            var toItem = to.item;
        
            InsertItem(from, toItem);
            InsertItem(to, fromItem);
        }
        
        private void Merge(InventorySlot from, InventorySlot to)
        {
            if (from == to)
                return;
            
            if (from.isEmpty)
                return;
            
            if (to.isEmpty)
                return;

            if (from.item.config.type != to.item.config.type)
                return;

            int totalCount = from.item.count + to.item.count;

            if (totalCount > from.item.config.maxCount)
            {
                to.item.count = to.item.config.maxCount;
                from.item.count = totalCount - to.item.count;
            }
            else
            {
                to.item.count = totalCount;
                ClearSlot(from);
            }
        }

        public void InsertItem(InventorySlot slot, ItemData itemData)
        {
            slot.Insert(itemData);
        }
        
        public void ClearSlot(InventorySlot slot)
        {
            slot.Clear();
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