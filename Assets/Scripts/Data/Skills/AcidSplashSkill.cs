using Configs.Skills;
using UnityEngine;

namespace Data.Skills
{
    public class AcidSplashSkill : Skill
    {
        public new readonly AcidSplashSkillConfig config;
        public AcidSplashSkill(AcidSplashSkillConfig config) : base(config)
        {
            this.config = config;
        }

        public override void Execute(Unit sender, Unit receiver)
        {
            var skillLevel = GetSkillLevel(sender, config.levels);
            
            var colliders = Physics.OverlapSphere(receiver.transform.position, skillLevel.radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                var unit = colliders[i].GetComponent<Unit>();
                if (unit != null)
                {
                    if (unit == sender)
                        continue;

                    unit.AddDamage(skillLevel.damage);
                    Debug.Log($"[AcidSplash] Added {skillLevel.damage} damage.");
                    unit.AddEffect(skillLevel.effectType, skillLevel.effectLevel);
                }
            }
        }
    }
}