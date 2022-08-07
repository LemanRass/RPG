using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace Skills.Core
{
    [Serializable]
    public class SkillTalentsRequirement
    {
        public TalentType talentType;
        public int level;
    }
    
    public abstract class Skill : ScriptableObject
    {
        public SkillType type;
        public List<SkillTalentsRequirement> requirements;
        public float cooldown;
        
        [HideInInspector] public bool isCoolingDown;
        [HideInInspector] public float cooldownTicks;

        public virtual void Update()
        {
            if (isCoolingDown)
            {
                cooldownTicks += Time.deltaTime;
                if (cooldownTicks >= cooldown)
                {
                    cooldownTicks = 0.0f;
                    isCoolingDown = false;
                }
            }
        }

        public virtual bool CheckIfCanUseSkill(Unit sender, Unit receiver)
        {
            /*if (isCoolingDown)
                return false;

            if (requirements.Any(n => n.level > sender.talents[n.talentType].level))
                return false;*/
            
            return true;
        }
        
        public abstract void Execute(Unit sender, Unit receiver);

        public void BeginCooldown()
        {
            cooldownTicks = 0.0f;
            isCoolingDown = true;
        }
    }
}