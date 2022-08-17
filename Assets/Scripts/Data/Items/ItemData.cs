using System;
using Configs.Items;

namespace Data.Items
{
    public abstract class ItemData
    {
        public event Action onChanged;
        
        public readonly ItemConfig config;

        private int _count;
        public int count
        {
            get => _count;
            set
            {
                _count = value;
                OnChanged();
            }
        }

        public ItemData(ItemConfig config)
        {
            this.config = config;
        }

        protected void OnChanged()
        {
            onChanged?.Invoke();
        }
    }
}