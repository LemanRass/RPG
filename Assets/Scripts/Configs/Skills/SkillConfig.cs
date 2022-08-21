using System;
using System.Collections.Generic;
using Data.Skills;
using Enums;
using UnityEngine;

namespace Configs.Skills
{
    [Serializable]
    public abstract class SkillLevel
    {
        public int talentLevel;
    }
    
    [Serializable]
    public class SkillTalentRequirement
    {
        public TalentType talentType;
        public int level;

        public bool IsOk(Unit unit)
        {
            return unit.talents[talentType].level >= level;
        }
    }
    
    public abstract class SkillConfig : ScriptableObject
    {
        public SkillType type;
        public string name;
        public Sprite icon;
        public AnimationClip anim;
        public TalentType talentType;
        public List<SkillTalentRequirement> requirements;
        public float castingDuration;
        public float cooldownDuration;
        
        public abstract Skill CreateInstance();
    }
}