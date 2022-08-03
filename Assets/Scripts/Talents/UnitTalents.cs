using System.Collections.Generic;
using Configs;
using Enums;
using Talents.Data;
using UnityEngine;

namespace Talents
{
    public class UnitTalents
    {
        private readonly Dictionary<TalentType, UnitTalentData> _talents;

        public UnitTalents(UnitTalentsData data)
        {
            _talents = new Dictionary<TalentType, UnitTalentData>();

            for (int i = 0; i < data.talents.Count; i++)
            {
                var talent = data.talents[i];
                _talents.Add(talent.type, talent);
            }
        }

        public void ApplyTalents(StatType statType, ref float value)
        {
            var talentType = ConfigsManager.statTalents[statType];
            var talentLevel = _talents[talentType].level;
            value += ConfigsManager.statLevels[statType][talentLevel - 1];
        }

        public void AddExperience(TalentType talentType, int exp)
        {
            if (IsTalentMaxLevel(talentType))
                return;
            
            var talent = _talents[talentType];
            var talentConfig = ConfigsManager.talents[talentType];

            talent.experience += exp;
            Debug.Log($"[{talentType}] Experience: {talent.experience}/{talentConfig.levels[talent.level].experience}");
            
            while (talent.experience >= talentConfig.levels[talent.level].experience)
            {
                if (IsTalentMaxLevel(talentType))
                {
                    Debug.Log($"[{talentType}] Already max level!");
                    return;
                }
                
                LevelUp(talentType);
            }
        }
        
        private void LevelUp(TalentType talentType)
        {
            var talent = _talents[talentType];
            var talentConfig = ConfigsManager.talents[talentType];
            
            talent.experience -= talentConfig.levels[talent.level].experience;
            _talents[talentType].level++;
            
            Debug.Log($"[{talentType.ToString()}] is {_talents[talentType].level} now. " +
                      $"Exp: {talent.experience}/{talentConfig.levels[talent.level].experience}");
        }

        private bool IsTalentMaxLevel(TalentType talentType)
        {
            var talent = _talents[talentType];
            var talentConfig = ConfigsManager.talents[talentType];

            return talent.level >= talentConfig.levels.Count - 1;
        }
    }
}