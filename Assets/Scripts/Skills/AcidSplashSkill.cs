using System;
using System.Collections.Generic;
using Enums;
using Skills.Core;
using UnityEngine;

namespace Skills
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
    public class AcidSplashSkill : Skill
    {
        public List<AcidSplashSkillLevel> levels;

        public override void Execute(Unit sender, Unit receiver)
        {
            var skillLevel = GetSkillLevel(sender, levels);
            
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