using System.Collections.Generic;
using Configs;
using Enums;
using Talents.Data;

namespace Talents
{
    public class UnitTalents
    {
        public readonly Dictionary<TalentType, UnitTalentData> talents;
        
        public UnitTalents(UnitTalentsData data)
        {
            talents = new Dictionary<TalentType, UnitTalentData>();

            for (int i = 0; i < data.talents.Count; i++)
            {
                var talent = data.talents[i];
                talents.Add(talent.type, talent);
            }
        }

        public void ApplyTalents(StatType statType, ref float value)
        {
            var talentType = ConfigsManager.statsLevels[statType].talentType;
            int talentLevel = talents[talentType].level;
            value += ConfigsManager.statsLevels[statType].values[talentLevel - 1];
        }
    }
}