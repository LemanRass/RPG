using System;
using System.Collections.Generic;
using Enums;
using Skills;
using Skills.Core;
using UnityEngine;

namespace Configs.Skills
{
    [Serializable]
    public class DamageBuffSkillLevel : SkillLevel
    {
        public EffectType effectType;
        public int effectLevel;
    }
    
    [CreateAssetMenu(fileName = "DamageBuffSkill", menuName = "Unit/Skills/DamageBuffSkill")]
    public class DamageBuffSkillConfig : SkillConfig
    {
        public List<DamageBuffSkillLevel> levels;
        
        public override Skill CreateInstance()
        {
            return new DamageBuffSkill(this);
        }
    }
}