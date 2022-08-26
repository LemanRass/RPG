using Data.Skills;
using UnityEngine;

namespace Configs.Skills
{
    [CreateAssetMenu(fileName = "ProjectileSkillConfig", menuName = "Unit/Skills/ProjectileSkillConfig")]
    public class ProjectileSkillConfig : SkillConfig
    {
        public GameObject projectilePrefab;
        public float projectileSpeed;
        
        public override Skill CreateInstance()
        {
            return new ProjectileSkill(this);
        }
    }
}