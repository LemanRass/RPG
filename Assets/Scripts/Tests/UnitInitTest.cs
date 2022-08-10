using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Equipment;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitInitTest
    {
        [UnityTest]
        public IEnumerator UnitInitializationProcess()
        {
            //Load player prefab
            var unitPrefab = Resources.Load<PlayerUnit>("Prefabs/Units/PlayerUnit");
            Assert.NotNull(unitPrefab);

            //Create our player
            var playerUnit = GameObject.Instantiate(unitPrefab);
            Assert.NotNull(playerUnit);

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
    }
}
