using System;
using System.Collections.Generic;
using Configs.Skills;
using UnityEngine;

namespace Data.Skills
{
    public abstract class Skill
    {
        public readonly SkillConfig config;

        public event Action onCooldownStarted;
        public event Action<float> onCooldownProgress;
        public event Action onCooldownFinished;
        
        public float cooldownTicks { get; private set; }
        public float cooldownProgress => cooldownTicks / config.cooldownDuration;
        public bool isCoolingDown { get; private set; }

        protected Skill(SkillConfig config)
        {
            this.config = config;
        }

        public void BeginCooldown()
        {
            cooldownTicks = 0.0f;
            isCoolingDown = true;
            onCooldownStarted?.Invoke();
        }

        public void FinishCooldown()
        {
            isCoolingDown = false;
            onCooldownFinished?.Invoke();
        }

        public virtual void Update()
        {
            if (isCoolingDown)
            {
                cooldownTicks += Time.deltaTime;
                onCooldownProgress?.Invoke(cooldownTicks);

                if (Mathf.Abs(cooldownTicks - config.cooldownDuration) < 0.01f)
                {
                    FinishCooldown();
                }
            }
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