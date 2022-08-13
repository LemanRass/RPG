using System;
using Configs.Items;
using Configs.Items.Core;

namespace Inventory
{
    public class InventorySlot
    {
        public event Action<Item> onChanged;
        public Item item;

        public bool isEmpty => item == null;

        public InventorySlot(Item item)
        {
            this.item = item;
        }
        
        public void Insert(Item item)
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