using System;
using Configs.Items;
using Configs.Items.Core;
using Inventory.Data;

namespace Inventory
{
    public class InventorySlot
    {
        public event Action<ItemData> onChanged;
        public ItemData item;

        public bool isEmpty => item == null;

        public InventorySlot(ItemData item)
        {
            this.item = item;
        }
        
        public void Insert(ItemData item)
        {
            this.item = item;
            onChanged?.Invoke(item);
        }

        public void Clear()
        {
            item = null;
            onChanged?.Invoke(item);
        }
    }
}