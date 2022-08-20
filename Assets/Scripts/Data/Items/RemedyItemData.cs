using Configs.Items;
using Enums;
using Interfaces;

namespace Data.Items
{
    public class RemedyItemData : ItemData, ICountableItem
    {
        public new readonly RemedyItemConfig config;
        public int something;
        
        public RemedyItemData(RemedyItemConfig config) : base(config)
        {
            this.config = config;
        }

        public void Use(Unit unit)
        {
            unit.AddEffect(config.effectType, config.effectLevel);
            count--;
            OnChanged();
        }
    }
}