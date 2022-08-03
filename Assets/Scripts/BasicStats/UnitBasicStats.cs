using System.Collections.Generic;
using BasicStats.Data;
using Enums;

namespace BasicStats
{
    public class UnitBasicStats
    {
        private readonly Dictionary<StatType, float> _stats;
        public float this[StatType type] => _stats[type];

        public UnitBasicStats(UnitStatsData data)
        {
            _stats = new Dictionary<StatType, float>();

            for (int i = 0; i < data.stats.Count; i++)
            {
                var stat = data.stats[i];
                _stats.Add(stat.type, stat.value);
            }
        }
    }
}