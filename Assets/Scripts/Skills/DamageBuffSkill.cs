using Configs.Skills;
using Skills.Core;

namespace Skills
{
    public class DamageBuffSkill : Skill
    {
        public new readonly DamageBuffSkillConfig config;
        
        public DamageBuffSkill(DamageBuffSkillConfig config) : base(config)
        {
            this.config = config;
        }
        
        public override void Execute(Unit sender, Unit receiver)
        {
            //var level = GetSkillLevel(sender, levels);
            //receiver.AddEffect(level.effectType, level.effectLevel);
        }
    }
}