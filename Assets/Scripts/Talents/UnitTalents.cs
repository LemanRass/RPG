using System.Collections.Generic;
using Enums;
using Talents.Configs;
using UnityEngine;

namespace Talents
{
    public class UnitTalents
    {
        private readonly Dictionary<TalentType, int> _talents;
        private readonly Dictionary<StatType, TalentType> _statTalents;
        private readonly Dictionary<StatType, List<float>> _statsTable;

        public UnitTalents(UnitTalentsConfig config)
        {
            _talents = new Dictionary<TalentType, int>();
            _statTalents = new Dictionary<StatType, TalentType>();
            _statsTable = new Dictionary<StatType, List<float>>();

            for (int i = 0; i < config.talents.Count; i++)
            {
                var talent = config.talents[i];
                _talents.Add(talent.type, 1);

                for (int j = 0; j < talent.stats.Count; j++)
                {
                    var stat = talent.stats[j];
                    _statTalents.Add(stat.type, talent.type);
                    _statsTable.Add(stat.type, new List<float>());

                    for (int k = 0; k < stat.levels.Count; k++)
                    {
                        _statsTable[stat.type].Add(stat.levels[k]);                        
                    }
                }
            }
        }

        public float GetStat(StatType statType)
        {
            var talentType = _statTalents[statType];
            var talentLevel = _talents[talentType];
            return _statsTable[statType][talentLevel - 1];
        }

        public void LevelUp(TalentType talentType)
        {
            _talents[talentType]++;
            Debug.Log($"[{talentType.ToString()}] is {_talents[talentType]} now.");
        }
    }
}