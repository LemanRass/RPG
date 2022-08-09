using System.Collections.Generic;
using Configs;
using Enums;
using Talents.Data;

namespace Talents
{
    public class UnitTalents
    {
        private readonly Dictionary<TalentType, UnitTalentData> _talents;
        public UnitTalentData this[TalentType talentType] => _talents[talentType];
        
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
            var talentType = ConfigsManager.statsLevels[statType].talentType;
            int talentLevel = _talents[talentType].level;
            value += ConfigsManager.statsLevels[statType].values[talentLevel - 1];
        }
    }
}