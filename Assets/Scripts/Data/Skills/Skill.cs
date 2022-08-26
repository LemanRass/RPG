using System;
using System.Collections.Generic;
using Components;
using Configs.Skills;
using UnityEngine;

namespace Data.Skills
{
    public abstract class Skill
    {
        public readonly SkillConfig config;
        public readonly CooldownComp cooldown;
   
        protected Skill(SkillConfig config)
        {
            this.config = config;
            cooldown = new CooldownComp(config.cooldownDuration);
        }

        public virtual void Update()
        {
            cooldown.Update();
        }
        
        public bool CheckExecuteRequirements(Unit sender)
        {
            for (int i = 0; i < config.requirements.Count; i++)
            {
                var requirement = config.requirements[i];

                if (!requirement.IsOk(sender))
                    return false;
            }

            return true;
        }
        
        public abstract void Execute(Unit sender, Unit receiver);

        protected T GetSkillLevel<T>(Unit sender, List<T> skillLevels) where T : SkillLevel
        {
            var senderTalentLevel = sender.talents[config.talentType].level;

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