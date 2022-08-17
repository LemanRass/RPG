using Configs.Items;

namespace Data.Items
{
    public class EquipmentItemData : ItemData
    {
        public new readonly EquipmentItemConfig config;

        private int _durability;
        public int durability
        {
            get => _durability;
            set
            {
                _durability = value;
                OnChanged();
            }
        }

        public EquipmentItemData(EquipmentItemConfig config) : base(config)
        {
            this.config = config;
        }
    }
}