using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Skills.Core
{
    public class UnitSkills
    {
        private readonly Dictionary<SkillType, Skill> _skills;
        private readonly List<SkillType> _skillsKeys;
        public Skill this[SkillType type] => _skills[type];

        public UnitSkills()
        {
            _skills = new Dictionary<SkillType, Skill>();
            _skillsKeys = new List<SkillType>();

            var skillsConfigs = Resources.LoadAll<Skill>("Configs/Skills");
            for (int i = 0; i < skillsConfigs.Length; i++)
            {
                var skill = skillsConfigs[i];
                _skills.Add(skill.type, skill);
                _skillsKeys.Add(skill.type);
            }
            
            Debug.Log($"Loaded {_skills.Count} skills.");
        }

        public void Update()
        {
            for (int i = 0; i < _skillsKeys.Count; i++)
            {
                var skill = _skills[_skillsKeys[i]];
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