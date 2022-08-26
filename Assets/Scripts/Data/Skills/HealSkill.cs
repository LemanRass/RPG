using Configs.Skills;

namespace Data.Skills
{
    public class HealSkill : Skill
    {
        public new readonly HealSkillConfig config;
        
        public HealSkill(HealSkillConfig config) : base(config)
        {
            this.config = config;
        }

        public override void Execute(Unit sender, Unit receiver)
        {
            receiver.AddEffect(config.effectType, config.effectLevel);
        }
    }
}