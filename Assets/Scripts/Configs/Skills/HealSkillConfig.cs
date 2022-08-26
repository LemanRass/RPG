using Data.Skills;
using Enums;
using UnityEngine;

namespace Configs.Skills
{
    [CreateAssetMenu(fileName = "HealSkillConfig", menuName = "Unit/Skills/HealSkillConfig")]
    public class HealSkillConfig : SkillConfig
    {
        public EffectType effectType;
        public int effectLevel;
        
        public override Skill CreateInstance()
        {
            return new HealSkill(this);
        }
    }
}