using Data.Skills;
using UnityEngine;
using Views;

namespace Configs.Skills
{
    [CreateAssetMenu(fileName = "ProjectileSkillConfig", menuName = "Unit/Skills/ProjectileSkillConfig")]
    public class ProjectileSkillConfig : SkillConfig
    {
        public SkillProjectileView projectilePrefab;
        public float projectileSpeed;
        
        public override Skill CreateInstance()
        {
            return new ProjectileSkill(this);
        }
    }
}