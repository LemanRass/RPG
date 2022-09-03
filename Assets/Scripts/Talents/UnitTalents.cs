using System;
using System.Collections.Generic;
using Configs;
using Enums;
using Talents.Data;
using UnityEngine;

namespace Talents
{
    public class UnitTalent
    {
        public event Action onExperienceChanged;
        public event Action onLevelChanged;

        public readonly TalentType talentType;

        public int level
        {
            get => _level;
            set
            {
                _level = value;
                onLevelChanged?.Invoke();
            }
        }
        private int _level;

        public int experience
        {
            get => _experience;
            set
            {
                _experience = value;
                onExperienceChanged?.Invoke();
            }
        }
        private int _experience;

        
        public UnitTalent(UnitTalentData data)
        {
            talentType = data.type;
            level = data.level;
            experience = data.experience;
        }
    }
    
    public class UnitTalents
    {
        private readonly Dictionary<TalentType, UnitTalent> _talents;
        private readonly List<TalentType> _talentsKeys;
        public UnitTalent this[TalentType talentType] => _talents[talentType];
        public UnitTalent this[int index] => _talents[_talentsKeys[index]];
        public int count => _talents.Count;

        public event Action onTalentsChanged;
        
        public UnitTalents(UnitTalentsData data)
        {
            _talents = new Dictionary<TalentType, UnitTalent>();
            _talentsKeys = new List<TalentType>();

            for (int i = 0; i < data.talents.Count; i++)
            {
                var talent = data.talents[i];
                _talents.Add(talent.type, new UnitTalent(talent));
                _talentsKeys.Add(talent.type);
            }
        }

        public void AddExperience(TalentType talentType, int exp)
        {
            //Debug.Log($"[BEFORE] Lvl: {_talents[talentType].level} ({_talents[talentType].experience}/{ConfigsManager.talentsLevels[talentType].experiences[_talents[talentType].level - 1]})");
            _talents[talentType].experience += exp;

            while (_talents[talentType].experience >= 
                   ConfigsManager.talentsLevels[talentType].experiences[_talents[talentType].level - 1])
            {
                _talents[talentType].experience -= ConfigsManager.talentsLevels[talentType]
                    .experiences[_talents[talentType].level - 1];
                _talents[talentType].level++;
                onTalentsChanged?.Invoke();
            }
            
            //Debug.Log($"[AFTER] Lvl: {_talents[talentType].level} ({_talents[talentType].experience}/{ConfigsManager.talentsLevels[talentType].experiences[_talents[talentType].level - 1]})");
        }
        
        public void ApplyTalents(StatType statType, ref float value)
        {
            if (ConfigsManager.statsLevels.ContainsKey(statType))
            {
                var talentType = ConfigsManager.statsLevels[statType].talentType;
                int talentLevel = _talents[talentType].level;
                value += ConfigsManager.statsLevels[statType].values[talentLevel - 1];
            }
        }
    }
}