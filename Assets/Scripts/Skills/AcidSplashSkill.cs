using Effects;
using Skills.Configs;
using UnityEngine;

namespace Skills
{
    public class AcidSplashSkill : Skill
    {
        public AcidSplashSkillConfig _config;
        
        public AcidSplashSkill(Unit sender, Unit receiver, SkillConfig config) : base(sender, receiver, config)
        {
            _config = config as AcidSplashSkillConfig;
            
            var colliders = Physics.OverlapSphere(receiver.transform.position, _config.radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                var unit = colliders[i].GetComponent<Unit>();
                if (unit != null)
                {
                    if (unit == sender)
                        continue;

                    var acidEffect = new AcidEffect(unit, _config.duration, _config.interval, _config.damage);
                    unit.AddEffect(acidEffect);
                }
            }
        }
    }
}