using Configs.Effects;

namespace Data.Effects
{
    public class SmallManaPotionEffect : Effect
    {
        public new readonly SmallManaPotionConfig config;
        
        public SmallManaPotionEffect(SmallManaPotionConfig config) : base(config)
        {
            this.config = config;
        }

        public override void Execute(Unit owner, int level)
        {
            owner.mana += config.mana;
            isFinished = true;
        }
    }
}