using System.Collections.Generic;
using Enums;
using Skills.Core;
using UnityEngine;

namespace Configs.Skills
{
    public abstract class SkillConfig : ScriptableObject
    {
        public SkillType type;
        public string name;
        public Sprite icon;
        public TalentType talentType;
        public List<TalentRequirement> requirements;
        public float cooldownDuration;
        
        public abstract Skill CreateInstance();
    }
}