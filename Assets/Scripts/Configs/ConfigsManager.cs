using System.Collections.Generic;
using Effects.Core;
using Enums;
using UnityEngine;

namespace Configs
{
    public static class ConfigsManager
    {
        public static Dictionary<TalentType, TalentLevelsConfig> talentsLevels { get; private set; }
        public static Dictionary<StatType, StatLevelsConfig> statsLevels { get; private set; }
        public static Dictionary<EffectType, Effect> effects { get; private set; }

        
        static ConfigsManager()
        {
            LoadTalents();
            LoadStats();
            LoadEffects();
        }

        private static void LoadEffects()
        {
            effects = new Dictionary<EffectType, Effect>();
            var effectsConfigs = Resources.LoadAll<Effect>("Configs/Effects");
            
            for (int i = 0; i < effectsConfigs.Length; i++)
            {
                var effect = effectsConfigs[i];
                effects.Add(effect.type, effect);
            }
            
            Debug.Log($"[ConfigsManager]Loaded {effects.Count} effects.");
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