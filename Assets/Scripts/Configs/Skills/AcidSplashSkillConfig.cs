using System;
using System.Collections.Generic;
using Data.Skills;
using Enums;
using UnityEngine;

namespace Configs.Skills
{
    [Serializable]
    public class AcidSplashSkillLevel : SkillLevel
    {
        public EffectType effectType;
        public int effectLevel;
        public float radius;
        public float damage;
    }
    
    [CreateAssetMenu(fileName = "AcidSplashSkill", menuName = "Unit/Skills/AcidSplashSkill")]
    public class AcidSplashSkillConfig : SkillConfig
    {
        public List<AcidSplashSkillLevel> levels;
        
        public override Skill CreateInstance()
        {
            return new AcidSplashSkill(this);
        }
    }
}