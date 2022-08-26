using Configs.Skills;
using UnityEngine;

namespace Data.Skills
{
    public class ProjectileSkill : Skill
    {
        public new readonly ProjectileSkillConfig config;

        private GameObject _projectile;
        
        public ProjectileSkill(ProjectileSkillConfig config) : base(config)
        {
            this.config = config;
        }

        public override void Execute(Unit sender, Unit receiver)
        {
            //_projectile = GameObject.Instantiate(config.projectilePrefab);
        }

        public override void Update()
        {
            base.Update();

            if (_projectile != null)
            {
                _projectile.transform.position +=
                    _projectile.transform.forward * (config.projectileSpeed * Time.deltaTime);
            }
        }
    }
}