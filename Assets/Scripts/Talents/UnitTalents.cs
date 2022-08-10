using System.Collections.Generic;
using Configs;
using Enums;
using Talents.Data;

namespace Talents
{
    public class UnitTalent
    {
        public TalentType talentType;
        public int level;
        public int experience;

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

        public void ApplyTalents(StatType statType, ref float value)
        {
            var talentType = ConfigsManager.statsLevels[statType].talentType;
            int talentLevel = _talents[talentType].level;
            value += ConfigsManager.statsLevels[statType].values[talentLevel - 1];
        }
    }
}