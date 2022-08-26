using Configs.Effects;

namespace Data.Effects
{
    public class HealEffect : Effect
    {
        public new readonly HealEffectConfig config;
        
        public HealEffect(HealEffectConfig config) : base(config)
        {
            this.config = config;
        }

        public override void Execute(Unit owner, int level)
        {
            owner.health += config.amountToHeal;
            isFinished = true;
        }
    }
}