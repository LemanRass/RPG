using Configs.Items;
using Interfaces;

namespace Data.Items
{
    public class ResourceItemData : ItemData, ICountableItem
    {
        public new readonly ResourceItemConfig config;

        public ResourceItemData(ResourceItemConfig config) : base(config)
        {
            this.config = config;
        }
    }
}