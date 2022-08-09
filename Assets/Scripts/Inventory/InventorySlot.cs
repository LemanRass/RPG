using System;
using Configs.Items;

namespace Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public Item item;

        public bool isEmpty => item == null;

        public void Insert(Item item)
        {
            this.item = item;
        }

        public void Clear()
        {
            item = null;
        }
    }
}