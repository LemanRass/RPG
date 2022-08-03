using Skills.Configs;

namespace Skills
{
    public class LightningSkill : Skill
    {
        public float damage = 10;
        
        public LightningSkill(Unit sender, Unit receiver, SkillConfig config) : base(sender, receiver, config)
        {
            receiver.AddDamage(damage);
        }
    }
}