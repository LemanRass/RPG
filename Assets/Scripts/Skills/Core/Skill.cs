using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Skills.Core
{
    public abstract class Skill : ScriptableObject
    {
        public SkillType type;
        public TalentType talentType;
        public List<TalentRequirement> requirements;
        public float cooldownDuration;
        
        [HideInInspector] public bool isCoolingDown;
        [HideInInspector] public float cooldownTicks;

        public virtual bool CheckIfCanUseSkill(Unit sender, Unit receiver)
        {
            if (isCoolingDown)
            {
                int ticksLeft = Mathf.RoundToInt(cooldownDuration - cooldownTicks); 
                Debug.Log($"Skill is currently cooling down. ({ticksLeft}s.)");
                return false;
            }

            if (!IsRequirementsOk(sender))
            {
                Debug.Log("Talent requirements are not met.");
                return false;
            }
            
            return true;
        }
        
        public abstract void Execute(Unit sender, Unit receiver);

        public void BeginCooldown()
        {
            cooldownTicks = 0.0f;
            isCoolingDown = true;
        }
        
        
        public virtual void Update()
        {
            if (isCoolingDown)
            {
                cooldownTicks += Time.deltaTime;
                if (cooldownTicks >= cooldownDuration)
                {
                    cooldownTicks = 0.0f;
                    isCoolingDown = false;
                }
            }
        }
        
        
        private bool IsRequirementsOk(Unit unit)
        {
            for (int i = 0; i < requirements.Count; i++)
            {
                var requirement = requirements[i];
                if (!requirement.IsOk(unit))
                    return false;
            }

            return true;
        }
        
        protected T GetSkillLevel<T>(Unit sender, List<T> skillLevels) where T : SkillLevel
        {
            var senderTalentLevel = sender.talents[talentType].level;

            T skillLevel = null;

            for (int i = 0; i < skillLevels.Count; i++)
            {
                if (senderTalentLevel < skillLevels[i].talentLevel)
                    break;

                skillLevel = skillLevels[i];
            }

            return skillLevel;
        }
    }
}