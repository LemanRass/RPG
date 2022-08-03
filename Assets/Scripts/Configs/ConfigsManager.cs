using System.Collections.Generic;
using Configs.Stats;
using Configs.Talents;
using Enums;
using Talents.Configs;
using UnityEngine;

namespace Configs
{
    public static class ConfigsManager
    {
        public static Dictionary<StatType, List<float>> statLevels { get; private set; }
        public static Dictionary<StatType, TalentType> statTalents { get; private set; }
        public static Dictionary<TalentType, TalentConfig> talents { get; private set; }

        
        static ConfigsManager()
        {
            LoadTalents();
            LoadStatsProgression();
        }

        private static void LoadTalents()
        {
            statTalents = new Dictionary<StatType, TalentType>();
            talents = new Dictionary<TalentType, TalentConfig>();
            statLevels = new Dictionary<StatType, List<float>>();
            
            var talentsConfig = Resources.Load<TalentsConfig>("Configs/UnitTalentsConfig");

            for (int i = 0; i < talentsConfig.talents.Count; i++)
            {
                var talent = talentsConfig.talents[i];
                talents.Add(talent.type, talent);

                for (int j = 0; j < talent.stats.Count; j++)
                {
                    var stat = talent.stats[j];
                    statTalents.Add(stat.type, talent.type);
                    
                    statLevels.Add(stat.type, new List<float>());
                    for (int k = 0; k < stat.levels.Count; k++)
                    {
                        var level = stat.levels[k];
                        statLevels[stat.type].Add(level.value);
                    }
                }
            }
        }
        
        private static void LoadStatsProgression()
        {
            statLevels = new Dictionary<StatType, List<float>>();
            var config = Resources.LoadAll<StatConfig>("Configs/Progressions");

            for (int i = 0; i < config.Length; i++)
            {
                var stat = config[i];
                statLevels.Add(stat.type, new List<float>());

                for (int j = 0; j < stat.levels.Count; j++)
                {
                    var level = stat.levels[j];
                    statLevels[stat.type].Add(level.value);
                }
            }
        }
    }
}