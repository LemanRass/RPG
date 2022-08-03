using Skills.Configs;
using UnityEngine;

namespace Skills
{
    public class ExplosionSkill : Skill
    {
        public float damage = 15;
        public float radius = 5;
        
        public ExplosionSkill(Unit sender, Unit receiver, SkillConfig config) : base(sender, receiver, config)
        {
            var colliders = Physics.OverlapSphere(receiver.transform.position, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                var unit = colliders[i].GetComponent<Unit>();
                if (unit != null)
                {
                    unit.AddDamage(damage);
                }
            }
        }
    }
}