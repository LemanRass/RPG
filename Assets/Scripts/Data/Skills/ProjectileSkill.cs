using Configs.Skills;
using UnityEngine;
using Views;

namespace Data.Skills
{
    public class ProjectileSkill : Skill
    {
        public new readonly ProjectileSkillConfig config;
        
        public ProjectileSkill(ProjectileSkillConfig config) : base(config)
        {
            this.config = config;
        }

        public override void Execute(Unit sender, Unit receiver)
        {
            var projectile = Object.Instantiate(config.projectilePrefab);
            var skillAnchor = ((PlayerUnit)sender).skills.GetSkillAnchor(config.vfx.anchorType);
            projectile.transform.position = projectile.transform.TransformPoint(skillAnchor.transform.position);
            projectile.transform.forward = skillAnchor.transform.forward;
            projectile.Init(receiver, this, () =>
            {
                receiver.AddDamage(1);
            });
        }
    }
}