using Configs.Items;
using Interfaces;

namespace Data.Items
{
    public class RemedyItemData : ItemData, ICountableItem
    {
        public new readonly RemedyItemConfig config;
        
        public RemedyItemData(RemedyItemConfig config) : base(config)
        {
            this.config = config;
        }

        public void Use(Unit unit)
        {
            unit.AddEffect(config.effect.type, config.effectLevel);
            count--;
            OnChanged();
        }
    }
}