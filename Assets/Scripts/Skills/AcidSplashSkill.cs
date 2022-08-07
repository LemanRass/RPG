using Effects;
using Enums;
using Skills.Core;
using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(fileName = "AcidSplashSkill", menuName = "Unit/Skills/AcidSplashSkill")]
    public class AcidSplashSkill : Skill
    {
        public EffectType effectType;
        public float radius;
        
        public override void Execute(Unit sender, Unit receiver)
        {
            var colliders = Physics.OverlapSphere(receiver.transform.position, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                var unit = colliders[i].GetComponent<Unit>();
                if (unit != null)
                {
                    if (unit == sender)
                        continue;

                    unit.AddDamage(10.0f);
                    unit.effects.AddEffect(effectType, 2);
                }
            }
        }
    }
}