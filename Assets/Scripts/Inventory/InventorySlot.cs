using System;
using Data.Items;

namespace Inventory
{
    public class InventorySlot
    {
        public event Action<ItemData> onChanged;
        public ItemData item { get; private set; }
        public bool isEmpty => item == null;

        public InventorySlot(ItemData item)
        {
            Insert(item);
        }
        
        public void Insert(ItemData item)
        {
            this.item = item;
            
            if (item != null)
            {
                item.onChanged += OnChanged;
            }
            
            OnChanged();
        }

        public void Clear()
        {
            if (item != null)
            {
                item.onChanged -= OnChanged;
            }
            
            item = null;
            OnChanged();
        }

        private void OnChanged()
        {
            onChanged?.Invoke(item);
        }
    }
}