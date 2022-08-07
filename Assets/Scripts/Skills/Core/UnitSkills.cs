using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Skills.Core
{
    public class UnitSkills
    {
        private readonly Dictionary<SkillType, Skill> _skills;
        public Skill this[SkillType type] => _skills[type];

        public UnitSkills()
        {
            _skills = new Dictionary<SkillType, Skill>();

            var skillsConfigs = Resources.LoadAll<Skill>("Configs/Skills");
            for (int i = 0; i < skillsConfigs.Length; i++)
            {
                var skill = skillsConfigs[i];
                _skills.Add(skill.type, skill);
            }
            
            Debug.Log($"Loaded {_skills.Count} skills.");
        }

        public void Update()
        {
            foreach (var skill in _skills.Values)
            {
                skill.Update();
            }
        }
        
        public void UseSkill(SkillType type, Unit sender, Unit receiver)
        {
            var skill = _skills[type];
            if (skill.CheckIfCanUseSkill(sender, receiver))
            {
                skill.Execute(sender, receiver);
                skill.BeginCooldown();
            }
        }
    }
}