using System;
using System.Collections.Generic;
using Enums;
using Skills.Core;
using UnityEngine;

namespace Skills
{
    [Serializable]
    public class DamageBuffSkillLevel : SkillLevel
    {
        public EffectType effectType;
        public int effectLevel;
    }
    
    [CreateAssetMenu(fileName = "DamageBuffSkill", menuName = "Unit/Skills/DamageBuffSkill")]
    public class DamageBuffSkill : Skill
    {
        public List<DamageBuffSkillLevel> levels;

        public override void Execute(Unit sender, Unit receiver)
        {
            var level = GetSkillLevel(sender, levels);
            receiver.AddEffect(level.effectType, level.effectLevel);
        }
    }
}