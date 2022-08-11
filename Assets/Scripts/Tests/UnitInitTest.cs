using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Enums;
using Equipment;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitInitTest
    {
        private PlayerUnit CreatePlayer()
        {
            //Load player prefab
            var unitPrefab = Resources.Load<PlayerUnit>("Prefabs/Units/PlayerUnit");
            Assert.NotNull(unitPrefab);

            //Create our player
            var playerUnit = GameObject.Instantiate(unitPrefab);
            Assert.NotNull(playerUnit);
            
            return playerUnit;
        }

        [UnityTest]
        public IEnumerator UnitInitializationProcess()
        {
            var playerUnit = CreatePlayer();

            //Wait one frame to let everything init
            yield return null;
            
            //Check if basic stats were loaded
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                Assert.NotNull(playerUnit.basicStats);
                Assert.NotNull(playerUnit.basicStats[statType]);
            }
            
            //Check for basic talents
            foreach (TalentType talentType in Enum.GetValues(typeof(TalentType)))
            {
                Assert.NotNull(playerUnit.talents);
                Assert.NotNull(playerUnit.talents[talentType]);
                Assert.AreEqual(1, playerUnit.talents[talentType].level);
            }
            
            //Check for basic equipment (should be empty)
            foreach (EquipmentType equipmentType in Enum.GetValues(typeof(EquipmentType)))
            {
                Assert.NotNull(playerUnit.equipment);
                Assert.NotNull(playerUnit.equipment[equipmentType]);
                Assert.Null(playerUnit.equipment[equipmentType].item);
            }

            //Basic inventory checks
            Assert.NotNull(playerUnit.inventory);
            Assert.Greater(playerUnit.inventory.capacity, 0);
            
            //Basic effects checks
            Assert.NotNull(playerUnit.effects);
            
            //Basic skills checks
            Assert.NotNull(playerUnit.skills);
        }

        [UnityTest]
        public IEnumerator UnitEffectsProcess()
        {
            var playerUnit = CreatePlayer();
            
            yield return null;

            var effects = playerUnit.effects;
            Assert.NotNull(effects);

            ValidateStats(playerUnit);

            effects.AddEffect(EffectType.DAMAGE_BUFF, 1);
            
            ValidateStats(playerUnit);
            
            effects.RemoveEffect(EffectType.DAMAGE_BUFF);
            
            ValidateStats(playerUnit);
        }

        public void ValidateStats(PlayerUnit playerUnit)
        {
            var actualStats = CalculateActualStats(playerUnit);
            var expectedStats = CalculateExpectedStats(playerUnit);
            Assert.NotNull(actualStats);
            Assert.NotNull(expectedStats);
            Assert.AreEqual(actualStats, expectedStats);
        }
        
        public bool DoesStatsMatch(Dictionary<StatType, float> actualStats, Dictionary<StatType, float> expectedStats)
        {
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                if (actualStats.ContainsKey(statType) && expectedStats.ContainsKey(statType))
                {
                    if (Mathf.Abs(actualStats[statType] - expectedStats[statType]) > 0.001f)
                    {
                        return false;
                    }

                    continue;
                }

                if (!actualStats.ContainsKey(statType) && !expectedStats.ContainsKey(statType))
                    continue;

                return false;
            }

            return true;
        }
        
        public Dictionary<StatType, float> CalculateActualStats(PlayerUnit playerUnit)
        {
            var stats = new Dictionary<StatType, float>();
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                stats.Add(statType, playerUnit.GetStat(statType));
            }
            return stats;
        }

        public Dictionary<StatType, float> CalculateExpectedStats(PlayerUnit playerUnit)
        {
            var stats = new Dictionary<StatType, float>();

            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                stats.Add(statType, playerUnit.basicStats[statType].value);
                
                //Manual apply equipment
                foreach (EquipmentType equipmentType in Enum.GetValues(typeof(EquipmentType)))
                {
                    var equipmentSlot = playerUnit.equipment[equipmentType];
                    
                    if (equipmentSlot.isEmpty)
                        continue;

                    foreach (var stat in equipmentSlot.item.stats)
                    {
                        if (stat.statType == statType)
                            stats[statType] += stat.value;
                    }
                }
                
                
                //Manual apply talents
                var talentType = ConfigsManager.statsLevels[statType].talentType;
                var talentLevel = playerUnit.talents[talentType].level;
                //Debug.Log($"Talent: {talentType} Lvl: {talentLevel} Stat: {statType} Value: {ConfigsManager.statsLevels[statType].values[talentLevel - 1]}");
                stats[statType] += ConfigsManager.statsLevels[statType].values[talentLevel - 1];
                
                //Manual apply effects
                float rawValue = 0.0f;
                float percentageValue = 0.0f;
                
                for (int i = 0; i < playerUnit.effects.count; i++)
                {
                    var effect = playerUnit.effects[i];

                    rawValue += effect.CalculateRawValue(statType);
                    percentageValue += effect.CalculatePercentageValue(statType);
                }

                stats[statType] += rawValue;
                stats[statType] *= 1.0f + percentageValue;
                
                //Debug.Log($"Total: {stats[statType]}");

            }

            return stats;
        }
    }
}
