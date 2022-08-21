using System;
using System.Collections.Generic;
using Data.Skills;
using Enums;

namespace Skills
{
    public class UnitSkills
    {
        private readonly Dictionary<SkillType, Skill> _skills;
        private readonly List<SkillType> _skillsKeys;

        public Skill this[SkillType skillType] => _skills[skillType];
        public Skill this[int index] => _skills[_skillsKeys[index]];
        public int count => _skills.Count;

        public SkillCaster skillCaster { get; private set; }
        
        
        public UnitSkills()
        {
            _skills = new Dictionary<SkillType, Skill>();
            _skillsKeys = new List<SkillType>();

            foreach (SkillType skillType in Enum.GetValues(typeof(SkillType)))
            {
                _skills.Add(skillType, Factory.Create(skillType));
                _skillsKeys.Add(skillType);
            }

            skillCaster = new SkillCaster();
        }

        public void Update()
        {
            for (int i = 0; i < _skillsKeys.Count; i++)
            {
                var key = _skillsKeys[i];
                _skills[key].Update();
            }
            
            skillCaster.Update();
        }
        
        public async void UseSkill(SkillType type, Unit sender, Unit receiver)
        {
            var skill = _skills[type];

            if (skill.cooldown.isCoolingDown)
                return;
            
            if (await skillCaster.Cast(skill))
            {
                skill.Execute(sender, receiver);
                skill.cooldown.Begin();
            }
        }
    }
}