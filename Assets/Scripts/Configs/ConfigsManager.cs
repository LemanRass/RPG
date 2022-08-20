using System.Collections.Generic;
using Configs.Skills;
using Enums;
using UnityEngine;

namespace Configs
{
    public static class ConfigsManager
    {
        public static Dictionary<TalentType, TalentLevelsConfig> talentsLevels { get; private set; }
        public static Dictionary<StatType, StatLevelsConfig> statsLevels { get; private set; }
        public static Dictionary<SkillType, SkillConfig> skills { get; private set; }

        static ConfigsManager()
        {
            LoadTalents();
            LoadStats();
            LoadSkills();
        }

        private static void LoadSkills()
        {
            skills = new Dictionary<SkillType, SkillConfig>();
            foreach (var skillConfig in Resources.LoadAll<SkillConfig>("Configs/Skills/"))
                skills.Add(skillConfig.type, skillConfig);
            
            Debug.Log($"[ConfigsManager]Loaded {skills.Count} skills.");
        }
        
        private static void LoadTalents()
        {
            talentsLevels = new Dictionary<TalentType, TalentLevelsConfig>();
            var talentsConfigs = Resources.LoadAll<TalentLevelsConfig>("Configs/Talents");

            for (int i = 0; i < talentsConfigs.Length; i++)
            {
                var talentConfig = talentsConfigs[i];
                talentsLevels.Add(talentConfig.talentType, talentConfig);
            }
            
            Debug.Log($"[ConfigsManager]Loaded {talentsLevels.Count} talents.");
        }

        private static void LoadStats()
        {
            statsLevels = new Dictionary<StatType, StatLevelsConfig>();
            var statsConfigs = Resources.LoadAll<StatLevelsConfig>("Configs/Stats");

            for (int i = 0; i < statsConfigs.Length; i++)
            {
                var statConfig = statsConfigs[i];
                statsLevels.Add(statConfig.statType, statConfig);
            }
            
            
            Debug.Log($"[ConfigsManager]Loaded {statsLevels.Count} stats.");
        }
    }
}