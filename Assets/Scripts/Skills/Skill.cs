using Skills.Configs;

namespace Skills
{
    public abstract class Skill
    {
        protected Skill(Unit sender, Unit receiver, SkillConfig config)
        {
        }
    }
}