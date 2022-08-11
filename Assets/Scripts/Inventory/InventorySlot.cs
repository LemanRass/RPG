using Configs.Items;

namespace Inventory
{
    public class InventorySlot
    {
        public Item item;

        public bool isEmpty => item == null;

        public InventorySlot(Item item)
        {
            this.item = item;
        }
        
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