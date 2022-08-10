using System.Collections.Generic;
using BasicStats.Data;
using Enums;

namespace BasicStats
{
    public struct BasicStat
    {
        public StatType statType;
        public float value;

        public BasicStat(StatType statType, float value)
        {
            this.statType = statType;
            this.value = value;
        }
    }
    
    public class UnitBasicStats
    {
        private readonly Dictionary<StatType, BasicStat> _stats;
        private readonly List<StatType> _statsKeys;

        public BasicStat this[StatType type] => _stats[type];
        public BasicStat this[int index] => _stats[_statsKeys[index]];
        public int count => _stats.Count;
        

        public UnitBasicStats(UnitStatsData data)
        {
            _stats = new Dictionary<StatType, BasicStat>();
            _statsKeys = new List<StatType>();

            for (int i = 0; i < data.stats.Count; i++)
            {
                var stat = data.stats[i];
                _stats.Add(stat.type, new BasicStat(stat.type, stat.value));
                _statsKeys.Add(stat.type);
            }
        }
    }
}